// Decompiled with JetBrains decompiler
// Type: Comum.ValidarMotivacao
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
  public class ValidarMotivacao : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string ID { get; set; }

    public string PLACA { get; set; }

    public string DTVALIDADEINICIAL { get; set; }

    public string DTVALIDADEFINAL { get; set; }

    public string DTSAIDA { get; set; }

    public ValidarMotivacao ValidaMotivacao(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      string v_Credencial)
    {
      ValidarMotivacao validarMotivacao1 = new ValidarMotivacao();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vCredencial", (object) v_Credencial, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<ValidarMotivacao> validarMotivacaos = this.Pesquisar<ValidarMotivacao>("BANCO", "SP_VALIDA_MOTIVACAO", "ValidarMotivacao.ValidarMotivacao", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (validarMotivacaos == null)
          return (ValidarMotivacao) null;
        foreach (ValidarMotivacao validarMotivacao2 in validarMotivacaos)
        {
          validarMotivacao1.ID = validarMotivacao2.ID;
          validarMotivacao1.PLACA = validarMotivacao2.PLACA;
          validarMotivacao1.DTVALIDADEINICIAL = validarMotivacao2.DTVALIDADEINICIAL;
          validarMotivacao1.DTVALIDADEFINAL = validarMotivacao2.DTVALIDADEFINAL;
          validarMotivacao1.DTSAIDA = validarMotivacao2.DTSAIDA;
        }
        return validarMotivacao1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro PegarConfiguracaoLAP(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (ValidarMotivacao) null;
      }
      finally
      {
        validarMotivacao1.Terminate();
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

    ~ValidarMotivacao()
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
