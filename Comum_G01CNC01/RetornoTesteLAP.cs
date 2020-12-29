// Decompiled with JetBrains decompiler
// Type: Comum.RetornoTesteLAP
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Comum
{
  public class RetornoTesteLAP : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public long ID_SECAO { get; set; }

    public string CD_CREDENCIAL { get; set; }

    public int VL_TEMPO_EXECUCAO { get; set; }

    public int VL_SCORE { get; set; }

    public byte[] IMAGEM { get; set; }

    public int ST_LIBERADO { get; set; }

    public long ID_REQUISICAO_PROBLEMA { get; set; }

    public IEnumerable<RetornoTesteLAP> RetornarTesteLAP(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      int v_secao)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdSecao", (object) v_secao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<RetornoTesteLAP> retornoTesteLaps = this.Pesquisar<RetornoTesteLAP>("BANCO", "SP_LAP_REQUISICAO_BUSCA", "RetornoTesteLAP.RetornarTesteLAP", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (retornoTesteLaps != null)
          return retornoTesteLaps;
        return (IEnumerable<RetornoTesteLAP>) null;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro RetornarTesteLAP(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (IEnumerable<RetornoTesteLAP>) null;
      }
      finally
      {
        this.Terminate();
      }
    }

    public byte[] RetornarImagemLAP(string v_s_Aplicacao, int v_Id_Equipamento, int v_secao)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("V_ID_SECAO", (object) v_secao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<RetornoTesteLBF> retornoTesteLbfs = this.Pesquisar<RetornoTesteLBF>("BANCO", "LAP.SP_LAP_REQUISICAO_BUSCA", "RetornoTesteLAP.RetornarImagemLAP", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (retornoTesteLbfs != null)
        {
          using (IEnumerator<RetornoTesteLBF> enumerator = retornoTesteLbfs.GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current.IMAGEM;
          }
        }
        return (byte[]) null;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro RetornarImagemLAP(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (byte[]) null;
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

    ~RetornoTesteLAP()
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
