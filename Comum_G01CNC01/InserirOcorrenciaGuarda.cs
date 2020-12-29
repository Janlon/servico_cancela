// Decompiled with JetBrains decompiler
// Type: Comum.InserirOcorrenciaGuarda
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Data;
using System.Diagnostics;

namespace Comum
{
  public class InserirOcorrenciaGuarda : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public void GravarOcorrencia(
      string v_i_Score_LAP,
      string v_i_Score_LBF,
      string v_Credencial_Veiculo,
      string v_Credencial_Pessoa,
      string v_Placa,
      string v_Secao,
      int v_Id_Equipamento,
      string v_s_Aplicacao,
      string v_Sentido,
      string v_VCO_Pessoa,
      string v_VCO_Veiculo)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("V_VL_SCORE_LAP", (object) v_i_Score_LAP.ToString(), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_VL_SCORE_LBF", (object) v_i_Score_LBF.ToString(), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_CREDENCIAL_VEICULO", (object) v_Credencial_Veiculo, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_CREDENCIAL_PESSOA", (object) v_Credencial_Pessoa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_PLACA_VEICULO", (object) v_Placa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_SECAO", (object) v_Secao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_ID_EQUIPAMENTO", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_SENTIDO", (object) v_Sentido, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_VCO_PESSOA", (object) v_VCO_Pessoa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("V_CD_VCO_VEICULO", (object) v_VCO_Veiculo, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_INTERVENCAO_GUARDA_INSERIR", (object) dynamicParameters, "InserirOcorrenciaGuarda.GravarOcorrencia", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro GravarOcorrencia(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~InserirOcorrenciaGuarda()
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
