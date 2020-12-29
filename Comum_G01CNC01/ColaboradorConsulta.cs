// Decompiled with JetBrains decompiler
// Type: Comum.ColaboradorConsulta
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
  public class ColaboradorConsulta : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string NOME { get; set; }

    public string FLAGDIRETOR { get; set; }

    public ColaboradorConsulta ColaboradorConsultar(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      string v_Credencial)
    {
      ColaboradorConsulta colaboradorConsulta1 = new ColaboradorConsulta();
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vCredencial", (object) v_Credencial, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<ColaboradorConsulta> colaboradorConsultas = this.Pesquisar<ColaboradorConsulta>("BANCO", "SP_Colaborador_Consulta", "ColaboradorConsulta.ColaboradorConsulta", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (colaboradorConsultas == null)
          return (ColaboradorConsulta) null;
        foreach (ColaboradorConsulta colaboradorConsulta2 in colaboradorConsultas)
        {
          colaboradorConsulta1.NOME = colaboradorConsulta2.NOME;
          colaboradorConsulta1.FLAGDIRETOR = colaboradorConsulta2.FLAGDIRETOR;
        }
        return colaboradorConsulta1;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro ColaboradorConsulta(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return (ColaboradorConsulta) null;
      }
      finally
      {
        colaboradorConsulta1.Terminate();
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

    ~ColaboradorConsulta()
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
