// Decompiled with JetBrains decompiler
// Type: Comum.VerificaListaNegra
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using AcessoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;

namespace Comum
{
  public class VerificaListaNegra : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string CD_REGISTRO { get; set; }

    public void VerificarListaNegra(
      string v_Credencial_Pessoa,
      string v_Credencial_Veiculo,
      string v_Sentido,
      string v_s_Aplicacao,
      string v_IP_Controladora,
      int v_Id_Equipamento,
      long v_Id_Secao)
    {
      try
      {
        OracleDynamicParameters dynamicParameters1 = new OracleDynamicParameters();
        dynamicParameters1.Add("V_CREDENCIAL_PESSOA", (object) v_Credencial_Pessoa, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters1.Add("V_CREDENCIAL_VEICULO", (object) v_Credencial_Veiculo, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters1.Add("V_LISTA_NEGRA_CONSULTA", (object) null, new OracleType?(OracleType.Cursor), new ParameterDirection?(ParameterDirection.Output), new int?());
        IEnumerable<VerificaListaNegra> verificaListaNegras = this.Pesquisar<VerificaListaNegra>("BANCO", "LISTA_NEGRA.SP_LISTA_NEGRA_CONSULTA", "VerificaListaNegra.VerificarListaNegra", (object) dynamicParameters1, CommandType.StoredProcedure, true);
        if (verificaListaNegras == null)
          return;
        foreach (VerificaListaNegra verificaListaNegra in verificaListaNegras)
        {
          OracleDynamicParameters dynamicParameters2 = new OracleDynamicParameters();
          dynamicParameters2.Add("V_CREDENCIAL", (object) verificaListaNegra.CD_REGISTRO.ToString(), new OracleType?(), new ParameterDirection?(), new int?());
          dynamicParameters2.Add("V_CD_SENTIDO", (object) v_Sentido, new OracleType?(), new ParameterDirection?(), new int?());
          dynamicParameters2.Add("V_ID_EQUIPAMENTO", (object) v_Id_Equipamento, new OracleType?(), new ParameterDirection?(), new int?());
          dynamicParameters2.Add("V_ID_SECAO", (object) v_Id_Secao, new OracleType?(), new ParameterDirection?(), new int?());
          this.Executar("BANCO", "LISTA_NEGRA.SP_LISTA_NEGRA_INSERT", (object) dynamicParameters2, "VerificaListaNegra.VerificarListaNegra", CommandType.StoredProcedure);
        }
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificarListaNegra(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
      }
      finally
      {
        this.Terminate();
      }
    }

    public void Desfazer_VerificarListaNegra(
      string v_Credencial_Pessoa,
      string v_Credencial_Veiculo,
      string v_Sentido,
      string v_s_Aplicacao,
      string v_IP_Controladora,
      int v_Id_Equipamento)
    {
      try
      {
        OracleDynamicParameters dynamicParameters1 = new OracleDynamicParameters();
        dynamicParameters1.Add("V_CREDENCIAL_PESSOA", (object) v_Credencial_Pessoa, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters1.Add("V_CREDENCIAL_VEICULO", (object) v_Credencial_Veiculo, new OracleType?(), new ParameterDirection?(), new int?());
        dynamicParameters1.Add("V_LISTA_NEGRA_CONSULTA", (object) null, new OracleType?(OracleType.Cursor), new ParameterDirection?(ParameterDirection.Output), new int?());
        IEnumerable<VerificaListaNegra> verificaListaNegras = this.Pesquisar<VerificaListaNegra>("BANCO", "LISTA_NEGRA.SP_LISTA_NEGRA_CONSULTA", "VerificaListaNegra.Desfazer_VerificarListaNegra", (object) dynamicParameters1, CommandType.StoredProcedure, true);
        if (verificaListaNegras == null)
          return;
        foreach (VerificaListaNegra verificaListaNegra in verificaListaNegras)
        {
          OracleDynamicParameters dynamicParameters2 = new OracleDynamicParameters();
          dynamicParameters2.Add("V_CREDENCIAL", (object) verificaListaNegra.CD_REGISTRO.ToString(), new OracleType?(), new ParameterDirection?(), new int?());
          dynamicParameters2.Add("V_CD_SENTIDO", (object) v_Sentido, new OracleType?(), new ParameterDirection?(), new int?());
          this.Executar("BANCO", "LISTA_NEGRA.SP_LISTA_NEGRA_DELETE", (object) dynamicParameters2, "VerificaListaNegra.Desfazer_VerificarListaNegra", CommandType.StoredProcedure);
        }
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro Desfazer_VerificarListaNegra(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~VerificaListaNegra()
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
