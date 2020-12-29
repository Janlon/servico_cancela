// Decompiled with JetBrains decompiler
// Type: Comum.GravaAcessoSphera
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using AcessoDados;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;

namespace Comum
{
  public class GravaAcessoSphera : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public void GravarAcessoSphera(
      string v_CD_CREDENCIAL,
      int v_ID_EQUIPAMENTO,
      string v_CD_SENTIDO,
      string v_s_Aplicacao,
      long V_ID_SECAO,
      string V_CD_TIPO_CREDENCIAL,
      string V_CD_VCO,
      string vl_Score = "00000")
    {
      try
      {
        OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
        dynamicParameters.Add("V_CREDENCIAL", (object) v_CD_CREDENCIAL, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add("V_ID_EQUIPAMENTO", (object) v_ID_EQUIPAMENTO, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add("V_CD_SENTIDO", (object) v_CD_SENTIDO, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add(nameof (V_ID_SECAO), (object) V_ID_SECAO, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add(nameof (V_CD_TIPO_CREDENCIAL), (object) V_CD_TIPO_CREDENCIAL, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters.Add(nameof (V_CD_VCO), (object) V_CD_VCO, new OracleType?(), new ParameterDirection?(), new int?());
        this.Executar("BANCO", "ACESSO_SPHERA.SP_ACESSO_CONTROLE", (object) dynamicParameters, "GravaAcesso.GravarAcesso", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro GravarAcesso() ID Controladora: " + v_ID_EQUIPAMENTO.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
      }
      finally
      {
        this.Terminate();
      }
    }

    public void DesgravarAcessoSphera(
      string v_CD_CREDENCIAL,
      int v_ID_EQUIPAMENTO,
      string v_CD_SENTIDO,
      string v_s_Aplicacao,
      long V_ID_SECAO,
      string V_CD_TIPO_CREDENCIAL,
      string V_CD_VCO)
    {
      try
      {
        OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
        dynamicParameters.Add(nameof (V_ID_SECAO), (object) V_ID_SECAO, new OracleType?(), new ParameterDirection?(), new int?());
        this.Executar("BANCO", "ACESSO_SPHERA.SP_ACESSO_CONTROLE_DESFAZER", (object) dynamicParameters, "GravaAcesso.DesgravarAcesso", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro DesgravarAcesso() ID Controladora: " + v_ID_EQUIPAMENTO.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~GravaAcessoSphera()
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
