// Decompiled with JetBrains decompiler
// Type: Comum.PegaConfiguracaoLAP
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
  public class PegaConfiguracaoLAP : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public int QTIMAGENSLAP { get; set; }

    public int VLNOTACORTELAP { get; set; }

    public string CDAPLICARCORRECAO { get; set; }

    public int QTTENTATIVAS { get; set; }

    public PegaConfiguracaoLAP PegarConfiguracaoLAP(
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      PegaConfiguracaoLAP pegaConfiguracaoLap1 = new PegaConfiguracaoLAP();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<PegaConfiguracaoLAP> pegaConfiguracaoLaps = this.Pesquisar<PegaConfiguracaoLAP>("BANCO", "SP_EQUIP_CONFIG_LAP", "PegaConfiguracaoLAP.PegarConfiguracaoLAP", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (pegaConfiguracaoLaps == null)
          return (PegaConfiguracaoLAP) null;
        foreach (PegaConfiguracaoLAP pegaConfiguracaoLap2 in pegaConfiguracaoLaps)
        {
          pegaConfiguracaoLap1.QTIMAGENSLAP = pegaConfiguracaoLap2.QTIMAGENSLAP;
          pegaConfiguracaoLap1.VLNOTACORTELAP = pegaConfiguracaoLap2.VLNOTACORTELAP;
          pegaConfiguracaoLap1.CDAPLICARCORRECAO = pegaConfiguracaoLap2.CDAPLICARCORRECAO;
          pegaConfiguracaoLap1.QTTENTATIVAS = pegaConfiguracaoLap2.QTTENTATIVAS;
        }
        return pegaConfiguracaoLap1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro PegarConfiguracaoLAP(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (PegaConfiguracaoLAP) null;
      }
      finally
      {
        pegaConfiguracaoLap1.Terminate();
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

    ~PegaConfiguracaoLAP()
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
