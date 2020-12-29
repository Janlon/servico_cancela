// Decompiled with JetBrains decompiler
// Type: Comum.PegaControladora
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
  public class PegaControladora : Base, IDisposable
  {
    private bool disposedValue = false;

    public string Ip { get; set; }

    public string NrPorta { get; set; }

    public string NrPortaSaida { get; set; }

    public PegaControladora PegarControladora(
      int v_Id_Equipamento,
      string v_s_Aplicacao)
    {
      PegaControladora pegaControladora1 = new PegaControladora();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<PegaControladora> pegaControladoras = this.Pesquisar<PegaControladora>("BANCO", "SP_EquipamentoConfiguracaoBuscar", "PegaControladora.PegarControladora", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (pegaControladoras == null)
          return (PegaControladora) null;
        foreach (PegaControladora pegaControladora2 in pegaControladoras)
        {
          pegaControladora1.Ip = pegaControladora2.Ip;
          pegaControladora1.NrPorta = pegaControladora2.NrPorta;
          pegaControladora1.NrPortaSaida = pegaControladora2.NrPortaSaida;
        }
        return pegaControladora1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro PegarControladora(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (PegaControladora) null;
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

    ~PegaControladora()
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
