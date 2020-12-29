// Decompiled with JetBrains decompiler
// Type: Comum.VerificaBalanca
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
  public class VerificaBalanca : Base, IDisposable
  {
    private bool disposedValue = false;

    public int FLAG_VERIFICA_BALANCA { get; set; }

    public int FLAG_OCUPADA { get; set; }

    public VerificaBalanca VerificarBalanca(
      int v_Id_Equipamento,
      string v_s_Aplicacao)
    {
      VerificaBalanca verificaBalanca1 = new VerificaBalanca();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<VerificaBalanca> verificaBalancas = this.Pesquisar<VerificaBalanca>("BANCO", "SP_EquipamentoConfiguracaoBalanca", "VerificaBalanca.VerificaBalanca", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (verificaBalancas == null)
          return (VerificaBalanca) null;
        foreach (VerificaBalanca verificaBalanca2 in verificaBalancas)
        {
          verificaBalanca1.FLAG_VERIFICA_BALANCA = verificaBalanca2.FLAG_VERIFICA_BALANCA;
          verificaBalanca1.FLAG_OCUPADA = verificaBalanca2.FLAG_OCUPADA;
        }
        return verificaBalanca1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificaBalanca(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (VerificaBalanca) null;
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

    ~VerificaBalanca()
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
