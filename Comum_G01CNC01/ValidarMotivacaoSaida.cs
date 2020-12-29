// Decompiled with JetBrains decompiler
// Type: Comum.ValidarMotivacaoSaida
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
  public class ValidarMotivacaoSaida : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string ID { get; set; }

    public string PLACA { get; set; }

    public string DTVALIDADEINICIAL { get; set; }

    public string DTVALIDADEFINAL { get; set; }

    public string DTSAIDA { get; set; }

    public ValidarMotivacaoSaida ValidaMotivacaoSaida(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      string v_Credencial)
    {
      ValidarMotivacaoSaida validarMotivacaoSaida1 = new ValidarMotivacaoSaida();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vCredencial", (object) v_Credencial, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<ValidarMotivacaoSaida> validarMotivacaoSaidas = this.Pesquisar<ValidarMotivacaoSaida>("BANCO", "SP_VALIDA_MOTIVACAO_SAIDA", "ValidarMotivacao.ValidarMotivacao", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (validarMotivacaoSaidas == null)
          return (ValidarMotivacaoSaida) null;
        foreach (ValidarMotivacaoSaida validarMotivacaoSaida2 in validarMotivacaoSaidas)
        {
          validarMotivacaoSaida1.ID = validarMotivacaoSaida2.ID;
          validarMotivacaoSaida1.PLACA = validarMotivacaoSaida2.PLACA;
          validarMotivacaoSaida1.DTVALIDADEINICIAL = validarMotivacaoSaida2.DTVALIDADEINICIAL;
          validarMotivacaoSaida1.DTVALIDADEFINAL = validarMotivacaoSaida2.DTVALIDADEFINAL;
          validarMotivacaoSaida1.DTSAIDA = validarMotivacaoSaida2.DTSAIDA;
        }
        return validarMotivacaoSaida1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro PegarConfiguracaoLAP(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (ValidarMotivacaoSaida) null;
      }
      finally
      {
        validarMotivacaoSaida1.Terminate();
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

    ~ValidarMotivacaoSaida()
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
