// Decompiled with JetBrains decompiler
// Type: Comum.PegaCamerasLBF
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
  public class PegaCamerasLBF : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public int NRPOSICAOCAMERATOTEM { get; set; }

    public string DSLINK { get; set; }

    public int CDDETECCAOFACIAL { get; set; }

    public int CDRECONHECIMENTOFACIAL { get; set; }

    public string IPEQUIPAMENTO { get; set; }

    public string NMUSERCAMERA { get; set; }

    public string CDPASSWORDCAMERA { get; set; }

    public string NRPORTAENTRADAEQUIPAMENTO { get; set; }

    public int VLROTACAOCAMERA { get; set; }

    public int CDFABRICANTE { get; set; }

    public List<PegaCamerasLBF> PegarCamerasLBF(
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      try
      {
        List<PegaCamerasLBF> pegaCamerasLbfList = new List<PegaCamerasLBF>();
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<PegaCamerasLBF> pegaCamerasLbfs = this.Pesquisar<PegaCamerasLBF>("BANCO", "SP_CAMERAS_CONSULTA_LBF", "PegaCamerasLBF.PegarCamerasLBF", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (pegaCamerasLbfs == null)
          return (List<PegaCamerasLBF>) null;
        foreach (PegaCamerasLBF pegaCamerasLbf in pegaCamerasLbfs)
          pegaCamerasLbfList.Add(pegaCamerasLbf);
        return pegaCamerasLbfList;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro PegarCamerasLBF(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (List<PegaCamerasLBF>) null;
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

    ~PegaCamerasLBF()
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
