// Decompiled with JetBrains decompiler
// Type: Comum.LocalizaCrachaMaster
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
  public class LocalizaCrachaMaster : Base, IDisposable
  {
    private bool disposedValue = false;

    public string CdAtivo { get; set; }

    public LocalizaCrachaMaster LocalizarCrachaMaster(
      string cracha,
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      try
      {
        LocalizaCrachaMaster localizaCrachaMaster1 = new LocalizaCrachaMaster();
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vCdCracha", (object) cracha, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<LocalizaCrachaMaster> source = this.Pesquisar<LocalizaCrachaMaster>("BANCO", "SP_CrachaLocalizaAcessoMaster", "LocalizaCrachaMaster.LocalizaCrachaMaster()", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (source == null || source.Count<LocalizaCrachaMaster>() <= 0)
          return (LocalizaCrachaMaster) null;
        foreach (LocalizaCrachaMaster localizaCrachaMaster2 in source)
          localizaCrachaMaster1.CdAtivo = localizaCrachaMaster2.CdAtivo;
        return localizaCrachaMaster1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro Secao(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (LocalizaCrachaMaster) null;
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

    ~LocalizaCrachaMaster()
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
