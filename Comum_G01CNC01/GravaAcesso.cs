// Decompiled with JetBrains decompiler
// Type: Comum.GravaAcesso
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Data;
using System.Diagnostics;

namespace Comum
{
  public class GravaAcesso : AcessoDados.AcessoDados, IDisposable
  {
    private Random rdn = new Random();
    private bool disposedValue = false;

    public void GravarAcesso(
      string v_CD_CREDENCIAL,
      int v_ID_EQUIPAMENTO,
      string v_CD_SENTIDO,
      string v_s_Aplicacao,
      long V_ID_SECAO,
      string V_CD_TIPO_CREDENCIAL,
      string V_CD_VCO,
      string CD_TIPO_EQUIPAMENTO,
      string vl_Score = "00000",
      int fator = 0,
      string vIdMotivacao = "0",
      string vIdManobra = "0",
      string vNumeroOS = "",
      string vPlaca = "",
      string vObs = "")
    {
      string str = "";
      try
      {
        str = "Credencial: " + v_CD_CREDENCIAL + ", Id Equipamento: " + (object) v_ID_EQUIPAMENTO + ", Sentido: " + v_CD_SENTIDO + ", Seção: " + (object) V_ID_SECAO + ", Tipo Credencial: " + V_CD_TIPO_CREDENCIAL + ", Vco: " + V_CD_VCO;
        DynamicParameters dynamicParameters1 = new DynamicParameters();
        dynamicParameters1.Add("vIdSecao", (object) V_ID_SECAO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add("vIdCredencial", (object) v_CD_CREDENCIAL, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add("vIdEquipamento", (object) v_ID_EQUIPAMENTO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add("vCdSentido", (object) v_CD_SENTIDO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add("vCdAcao", (object) V_CD_VCO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add(nameof (vIdMotivacao), (object) vIdMotivacao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add("vNumeroOs", (object) vNumeroOS, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add(nameof (vPlaca), (object) vPlaca, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters1.Add(nameof (vObs), (object) vObs, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_ACESSO_CONTROLE_OBS", (object) dynamicParameters1, "GravaAcesso.GravarAcesso", CommandType.StoredProcedure);
        if (V_CD_VCO == "001" || V_CD_VCO == "002")
          new AcessoTemporario().ExcluirAcessoTemporario(v_CD_CREDENCIAL, v_s_Aplicacao, v_ID_EQUIPAMENTO);
        if (!(vIdManobra != "0"))
          return;
        DynamicParameters dynamicParameters2 = new DynamicParameters();
        dynamicParameters2.Add("vIdSecao", (object) V_ID_SECAO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters2.Add("vId", (object) vIdManobra, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_ManobrarVeiculosEditar", (object) dynamicParameters2, "GravaAcesso.AlterarManobra", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro GravarAcesso() ID Controladora: " + v_ID_EQUIPAMENTO.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message + " - Dados Enviados: " + str + "vl_Score: " + vl_Score, EventLogEntryType.Error, ex);
      }
      finally
      {
        this.Terminate();
      }
    }

    public void DesgravarAcesso(
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
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdSecao", (object) V_ID_SECAO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_ACESSO_CONTROLE_DESFAZER", (object) dynamicParameters, "GravaAcesso.DesgravarAcesso", CommandType.StoredProcedure);
        new AcessoTemporario().ExcluirAcessoTemporario(v_CD_CREDENCIAL, v_s_Aplicacao, v_ID_EQUIPAMENTO);
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

    public void InserirAcesso(
      string v_CD_CREDENCIAL,
      int v_ID_EQUIPAMENTO,
      string v_CD_SENTIDO,
      string v_s_Aplicacao,
      long V_ID_SECAO,
      string V_CD_TIPO_CREDENCIAL,
      string V_CD_VCO,
      string CD_TIPO_EQUIPAMENTO,
      string vl_Score = "00000",
      int fator = 0,
      string vIdMotivacao = "0",
      string vIdManobra = "0",
      string vNumeroOS = "",
      string vPlaca = "",
      string V_DATA_ACESSO = "")
    {
      string str = "";
      try
      {
        str = "Credencial: " + v_CD_CREDENCIAL + ", Id Equipamento: " + (object) v_ID_EQUIPAMENTO + ", Sentido: " + v_CD_SENTIDO + ", Seção: " + (object) V_ID_SECAO + ", Tipo Credencial: " + V_CD_TIPO_CREDENCIAL + ", Vco: " + V_CD_VCO;
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdSecao", (object) V_ID_SECAO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdCredencial", (object) v_CD_CREDENCIAL, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdEquipamento", (object) v_ID_EQUIPAMENTO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vCdSentido", (object) v_CD_SENTIDO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vCdAcao", (object) V_CD_VCO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add(nameof (vIdMotivacao), (object) vIdMotivacao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vNumeroOs", (object) vNumeroOS, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add(nameof (vPlaca), (object) vPlaca, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vData", (object) V_DATA_ACESSO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_ACESSO_CONTROLE_DATE", (object) dynamicParameters, "GravaAcesso.GravarAcesso", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro GravarAcesso() ID Controladora: " + v_ID_EQUIPAMENTO.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message + " - Dados Enviados: " + str + "vl_Score: " + vl_Score, EventLogEntryType.Error, ex);
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

    ~GravaAcesso()
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
