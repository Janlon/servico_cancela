// Decompiled with JetBrains decompiler
// Type: Comum.Logar
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using Infra;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace Comum
{
  public class Logar : Base, IDisposable
  {
    private Dominio.Log vLog = new Dominio.Log();
    private bool disposedValue = false;

    public void ILogar(
      string v_s_Aplicacao,
      string v_Secao,
      int v_Id_Equipamento,
      int v_idMensagem,
      string v_Credencial,
      string v_Mensagem_Livre,
      string v_DataHoraControladora,
      string v_Sentido = "",
      string v_Cliente = "")
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        this.vLog._ID_TIPO_LOG(7)._ID_EQUIPAMENTO(new int?(v_Id_Equipamento))._NM_APLICACAO(v_s_Aplicacao)._ID_CRITICIDADE(1)._ID_MENSAGEM(v_idMensagem)._ID_CREDENCIAL(v_Credencial)._DS_MENSAGEM_LIVRE(v_Mensagem_Livre)._CD_SENTIDO(v_Sentido);
        dynamicParameters.Add("vIdTipoLog", (object) this.vLog.ID_TIPO_LOG, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdMensagem", (object) this.vLog.ID_MENSAGEM, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        if (v_Secao != "")
        {
          if (v_Secao != null)
            dynamicParameters.Add("vIdSecao", (object) long.Parse(v_Secao), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
          else
            dynamicParameters.Add("vIdSecao", (object) null, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        }
        else
          dynamicParameters.Add("vIdSecao", (object) null, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdCredencial", (object) this.vLog.ID_CREDENCIAL.ToString(), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdEquipamento", (object) this.vLog.ID_EQUIPAMENTO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vNmAplicacao", (object) this.vLog.NM_APLICACAO.ToString(), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdCriticidade", (object) this.vLog.ID_CRITICIDADE, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vDsMensagemLivre", (object) this.vLog.DS_MENSAGEM_LIVRE.ToString(), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vCdSentido", (object) this.vLog.CD_SENTIDO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        DateTime result;
        bool exact = DateTime.TryParseExact(v_DataHoraControladora, "dd/MM/yyyy hh:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        if (v_DataHoraControladora != null & exact)
          dynamicParameters.Add("vDtControladora", (object) DateTime.Parse(v_DataHoraControladora), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        else
          dynamicParameters.Add("vDtControladora", (object) null, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vComando", (object) null, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        new ILog().Executar("BANCO", "SP_LogInserir", (object) dynamicParameters, v_Cliente, CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro gravando Log Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~Logar()
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
