// Decompiled with JetBrains decompiler
// Type: Comum.VerificaVagas
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
  public class VerificaVagas : Base, IDisposable
  {
    private bool disposedValue = false;

    public int FLAG_OK { get; set; }

    public VerificaVagas VerificarVagas(
      int v_Id_Equipamento,
      string v_s_Aplicacao,
      int v_Area)
    {
      VerificaVagas verificaVagas1 = new VerificaVagas();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vArea", (object) v_Area, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<VerificaVagas> verificaVagases = this.Pesquisar<VerificaVagas>("BANCO", "SP_VerificaVagas", "VerificaVagas.VerificaVagas", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (verificaVagases == null)
          return (VerificaVagas) null;
        foreach (VerificaVagas verificaVagas2 in verificaVagases)
          verificaVagas1.FLAG_OK = verificaVagas2.FLAG_OK > 0 ? 0 : 1;
        return verificaVagas1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificaVaga(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (VerificaVagas) null;
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

    ~VerificaVagas()
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
