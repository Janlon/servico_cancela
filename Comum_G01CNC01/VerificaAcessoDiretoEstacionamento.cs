// Decompiled with JetBrains decompiler
// Type: Comum.VerificaAcessoDiretoEstacionamento
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using AcessoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;

namespace Comum
{
  public class VerificaAcessoDiretoEstacionamento : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string ID_CREDENCIAL_PESSOA { get; set; }

    public bool VerificarAcessoDiretoEstacionamento(
      string v_Credencial_Pessoa,
      string v_Id_Equipamento,
      string v_s_Aplicacao,
      string v_IP_Controladora)
    {
      try
      {
        OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
        dynamicParameters.Add("V_CREDENCIAL", (object) v_Credencial_Pessoa, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add("V_ID_EQUIPAMENTO", (object) 106, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add("V_INTEGRA_ESTACIONAMENTO_C", (object) null, new OracleType?(OracleType.Cursor), new ParameterDirection?(ParameterDirection.Output), new int?());
        IEnumerable<VerificaAcessoDiretoEstacionamento> source = this.Pesquisar<VerificaAcessoDiretoEstacionamento>("BANCO", "INTEGRA_ESTACIONAMENTO.SP_INTEGRA_ESTACIONAMENTO_C", "VerificaAcessoDiretoEstacionamento.VerificarAcessoDiretoEstacionamento", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (source.Count<VerificaAcessoDiretoEstacionamento>() > 0)
        {
          using (IEnumerator<VerificaAcessoDiretoEstacionamento> enumerator = source.GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              VerificaAcessoDiretoEstacionamento current = enumerator.Current;
              return true;
            }
          }
        }
        return false;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificarAcessoDiretoEstacionamento(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return false;
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

    ~VerificaAcessoDiretoEstacionamento()
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
