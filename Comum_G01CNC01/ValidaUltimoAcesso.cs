// Decompiled with JetBrains decompiler
// Type: Comum.ValidaUltimoAcesso
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
  public class ValidaUltimoAcesso : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string RET { get; set; }

    public string CREDENCIAL { get; set; }

    public long ID { get; set; }

    public ValidaUltimoAcesso ValidarUltimoAcesso(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      string v_placa)
    {
      ValidaUltimoAcesso validaUltimoAcesso1 = new ValidaUltimoAcesso();
      try
      {
        if (v_s_Aplicacao == "G03CNC01")
        {
          DynamicParameters dynamicParameters = new DynamicParameters();
          dynamicParameters.Add("vplaca", (object) v_placa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
          IEnumerable<ValidaUltimoAcesso> validaUltimoAcessos = this.Pesquisar<ValidaUltimoAcesso>("BANCO", "SP_VALIDA_ULTIMO_ACESSO", "ValidaUltimoAcesso.ValidaUltimoAcesso", (object) dynamicParameters, CommandType.StoredProcedure, true);
          if (validaUltimoAcessos == null)
            return (ValidaUltimoAcesso) null;
          foreach (ValidaUltimoAcesso validaUltimoAcesso2 in validaUltimoAcessos)
          {
            validaUltimoAcesso1.RET = validaUltimoAcesso2.RET;
            validaUltimoAcesso1.CREDENCIAL = validaUltimoAcesso2.CREDENCIAL;
            validaUltimoAcesso1.ID = validaUltimoAcesso2.ID;
          }
          return validaUltimoAcesso1;
        }
        DynamicParameters dynamicParameters1 = new DynamicParameters();
        dynamicParameters1.Add("vplaca", (object) v_placa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<ValidaUltimoAcesso> validaUltimoAcessos1 = this.Pesquisar<ValidaUltimoAcesso>("BANCO", "[SP_VALIDA_MOTIVACAO_PLACA]", "ValidaUltimoAcesso.ValidaUltimoAcesso", (object) dynamicParameters1, CommandType.StoredProcedure, true);
        if (validaUltimoAcessos1 == null)
          return (ValidaUltimoAcesso) null;
        foreach (ValidaUltimoAcesso validaUltimoAcesso2 in validaUltimoAcessos1)
        {
          validaUltimoAcesso1.RET = "";
          validaUltimoAcesso1.CREDENCIAL = validaUltimoAcesso2.CREDENCIAL;
          validaUltimoAcesso1.ID = validaUltimoAcesso2.ID;
        }
        return validaUltimoAcesso1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro v_ValidarUltimoAcesso(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (ValidaUltimoAcesso) null;
      }
      finally
      {
        validaUltimoAcesso1.Terminate();
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

    ~ValidaUltimoAcesso()
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
