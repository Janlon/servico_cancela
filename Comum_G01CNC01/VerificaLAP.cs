// Decompiled with JetBrains decompiler
// Type: Comum.VerificaLAP
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
  public class VerificaLAP : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string CdExecutaLap { get; set; }

    public int VlNotaCorteLap { get; set; }

    public VerificaLAP VerificarLAP(string v_s_Aplicacao, int v_Id_Equipamento)
    {
      try
      {
        VerificaLAP verificaLap1 = new VerificaLAP();
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<VerificaLAP> verificaLaps = this.Pesquisar<VerificaLAP>("BANCO", "SP_VerificaLap", "VerificaLAP.VerificaLAP", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (verificaLaps == null)
          return (VerificaLAP) null;
        foreach (VerificaLAP verificaLap2 in verificaLaps)
        {
          verificaLap1.CdExecutaLap = verificaLap2.CdExecutaLap;
          verificaLap1.VlNotaCorteLap = verificaLap2.VlNotaCorteLap;
        }
        return verificaLap1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificarLAP(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (VerificaLAP) null;
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

    ~VerificaLAP()
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
