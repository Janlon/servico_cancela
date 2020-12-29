// Decompiled with JetBrains decompiler
// Type: Comum.LocalizaCracha
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Comum
{
  public class LocalizaCracha : Base, IDisposable
  {
    private bool disposedValue = false;

    public string CdAtivo { get; set; }

    public long IdCracha { get; set; }

    public string CdCracha { get; set; }

    public DateTime? DtValidadeInicial { get; set; }

    public DateTime? DtValidadeFinal { get; set; }

    public string CdSituacao { get; set; }

    public int IdTipoCracha { get; set; }

    public string CdVerificaDigital { get; set; }

    public string CdSenhaAcesso { get; set; }

    public string CdSenhaPanico { get; set; }

    public string CdReles { get; set; }

    public string CdSequenciaAcesso { get; set; }

    public string CdPosicaoCartao { get; set; }

    public string DsEspacos { get; set; }

    public string CdSenhaSegura { get; set; }

    public string NmUsuario { get; set; }

    public string Display1 { get; set; }

    public string Display2 { get; set; }

    public string placa { get; set; }

    public LocalizaCracha LocalizarCracha(
      string cracha,
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      try
      {
        LocalizaCracha localizaCracha1 = new LocalizaCracha();
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vCdCracha", (object) cracha, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<LocalizaCracha> source = this.Pesquisar<LocalizaCracha>("BANCO", "SP_CrachaLocalizaAcesso", "LocalizaCracha.LocalizarCracha()", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (source == null || source.Count<LocalizaCracha>() <= 0)
          return (LocalizaCracha) null;
        foreach (LocalizaCracha localizaCracha2 in source)
        {
          localizaCracha1.CdAtivo = localizaCracha2.CdAtivo;
          localizaCracha1.IdCracha = localizaCracha2.IdCracha;
          localizaCracha1.CdCracha = localizaCracha2.CdCracha;
          localizaCracha1.DtValidadeInicial = localizaCracha2.DtValidadeInicial;
          localizaCracha1.DtValidadeFinal = localizaCracha2.DtValidadeFinal;
          localizaCracha1.CdSituacao = localizaCracha2.CdSituacao;
          localizaCracha1.IdTipoCracha = localizaCracha2.IdTipoCracha;
          localizaCracha1.CdVerificaDigital = localizaCracha2.CdVerificaDigital;
          localizaCracha1.CdSenhaAcesso = localizaCracha2.CdSenhaAcesso;
          localizaCracha1.CdSenhaPanico = localizaCracha2.CdSenhaPanico;
          localizaCracha1.CdReles = localizaCracha2.CdReles;
          localizaCracha1.CdSequenciaAcesso = localizaCracha2.CdSequenciaAcesso;
          localizaCracha1.CdPosicaoCartao = localizaCracha2.CdPosicaoCartao;
          localizaCracha1.DsEspacos = localizaCracha2.DsEspacos;
          localizaCracha1.CdSenhaSegura = localizaCracha2.CdSenhaSegura;
          localizaCracha1.NmUsuario = localizaCracha2.NmUsuario;
          localizaCracha1.Display1 = localizaCracha2.Display1;
          localizaCracha1.Display2 = localizaCracha2.Display2;
        }
        return localizaCracha1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro Secao(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (LocalizaCracha) null;
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

    ~LocalizaCracha()
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
