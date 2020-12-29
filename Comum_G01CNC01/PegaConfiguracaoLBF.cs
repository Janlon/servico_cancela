// Decompiled with JetBrains decompiler
// Type: Comum.PegaConfiguracaoLBF
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
  public class PegaConfiguracaoLBF : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public int QTIMAGENSLBF { get; set; }

    public int QTTENTATIVAS { get; set; }

    public int VLNOTACORTELBF { get; set; }

    public string CDAPLICARCORRECAO { get; set; }

    public PegaConfiguracaoLBF PegarConfiguracaoLBF(
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      PegaConfiguracaoLBF pegaConfiguracaoLbf1 = new PegaConfiguracaoLBF();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<PegaConfiguracaoLBF> pegaConfiguracaoLbfs = this.Pesquisar<PegaConfiguracaoLBF>("BANCO", "SP_EQUIP_CONFIG_LBF", "PegaConfiguracaoLBF.PegaConfiguracaoLBF", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (pegaConfiguracaoLbfs == null)
          return (PegaConfiguracaoLBF) null;
        foreach (PegaConfiguracaoLBF pegaConfiguracaoLbf2 in pegaConfiguracaoLbfs)
        {
          pegaConfiguracaoLbf1.QTIMAGENSLBF = pegaConfiguracaoLbf2.QTIMAGENSLBF;
          pegaConfiguracaoLbf1.VLNOTACORTELBF = pegaConfiguracaoLbf2.VLNOTACORTELBF;
          pegaConfiguracaoLbf1.CDAPLICARCORRECAO = pegaConfiguracaoLbf2.CDAPLICARCORRECAO;
          pegaConfiguracaoLbf1.QTTENTATIVAS = pegaConfiguracaoLbf2.QTTENTATIVAS;
        }
        return pegaConfiguracaoLbf1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro PegarConfiguracaoLBF(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (PegaConfiguracaoLBF) null;
      }
      finally
      {
        pegaConfiguracaoLbf1.Terminate();
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

    ~PegaConfiguracaoLBF()
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
