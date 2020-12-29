// Decompiled with JetBrains decompiler
// Type: Comum.VerificaLBF
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
  public class VerificaLBF : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string CDEXECUTALBF { get; set; }

    public int VLNOTACORTELBF { get; set; }

    public VerificaLBF VerificarLBF(string v_s_Aplicacao, int v_Id_Equipamento)
    {
      try
      {
        VerificaLBF verificaLbf1 = new VerificaLBF();
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<VerificaLBF> verificaLbfs = this.Pesquisar<VerificaLBF>("BANCO", "SP_EQUIP_CONFIG_EXE_LBF", "VerificaLBF.VerificarLBF", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (verificaLbfs == null)
          return (VerificaLBF) null;
        foreach (VerificaLBF verificaLbf2 in verificaLbfs)
        {
          verificaLbf1.CDEXECUTALBF = verificaLbf2.CDEXECUTALBF;
          verificaLbf1.VLNOTACORTELBF = verificaLbf2.VLNOTACORTELBF;
        }
        return verificaLbf1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificarLBF(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (VerificaLBF) null;
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

    ~VerificaLBF()
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
