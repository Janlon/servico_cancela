// Decompiled with JetBrains decompiler
// Type: Comum.LocalizaVeiculo
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Comum
{
  public class LocalizaVeiculo : Base, IDisposable
  {
    private bool disposedValue = false;

    public string Placa { get; set; }

    public LocalizaVeiculo LocalizarVeiculo(
      string placa,
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      try
      {
        LocalizaVeiculo localizaVeiculo1 = new LocalizaVeiculo();
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vPlaca", (object) placa, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<LocalizaVeiculo> source = this.Pesquisar<LocalizaVeiculo>("BANCO", "SP_VeiculoLocalizar", "LocalizaVeiculo.LocalizarVeiculo()", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (source == null || source.Count<LocalizaVeiculo>() <= 0)
          return (LocalizaVeiculo) null;
        foreach (LocalizaVeiculo localizaVeiculo2 in source)
          localizaVeiculo1.Placa = localizaVeiculo2.Placa;
        return localizaVeiculo1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro Secao(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (LocalizaVeiculo) null;
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

    ~LocalizaVeiculo()
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
