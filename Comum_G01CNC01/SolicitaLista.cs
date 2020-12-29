// Decompiled with JetBrains decompiler
// Type: Comum.SolicitaLista
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using AcessoDados;
using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;

namespace Comum
{
  public class SolicitaLista : AcessoDados.AcessoDados, IDisposable
  {
    private static readonly int v_Id_Equipamento = int.Parse(ConfigurationManager.AppSettings["ID_EQUIPAMENTO"].ToString());
    private string v_s_Aplicacao = ConfigurationManager.AppSettings["APLICACAO"].ToString();
    private bool disposedValue = false;

    private string V_RETORNO { get; set; }

    public string SolicitarLista(int id)
    {
      try
      {
        OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
        dynamicParameters.Add("P_EQUIPAMENTO", (object) SolicitaLista.v_Id_Equipamento, new OracleType?(), new ParameterDirection?(), new int?());
        this.Executar("BANCO", "PKG_CARGA_CONTROLADORA.CARGA_FULL_CONTROLADORA", (object) dynamicParameters, "AcessoVeículoService.ReceberISPS()", CommandType.StoredProcedure);
        return "OK";
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro SolicitarLista(). ID Controladora: " + SolicitaLista.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return "NOK";
      }
      finally
      {
        this.Terminate();
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      if (!disposing)
        ;
      this.disposedValue = true;
    }

    ~SolicitaLista()
    {
      this.Dispose(false);
    }

    void IDisposable.Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public void Terminate()
    {
      this.Dispose(true);
    }
  }
}
