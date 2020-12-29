// Decompiled with JetBrains decompiler
// Type: Comum.AcessoTemporario
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
  public class AcessoTemporario : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string CD_CREDENCIAL { get; set; }

    public void InserirAcessoTemporario(
      string v_CD_CREDENCIAL,
      string v_s_Aplicacao,
      int v_id_equipamento)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("V_CD_CREDENCIAL", (object) v_CD_CREDENCIAL, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_ACESSO_TEMPORARIO_INSERIR", (object) dynamicParameters, "AcessoTemporario.GravarAcessoTemporario", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro GravarAcessoTemporario() ID Controladora: " + v_id_equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
      }
      finally
      {
        this.Terminate();
      }
    }

    public void ExcluirAcessoTemporario(
      string v_CD_CREDENCIAL,
      string v_s_Aplicacao,
      int v_id_equipamento)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("V_CD_CREDENCIAL", (object) v_CD_CREDENCIAL, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        this.Executar("BANCO", "SP_ACESSO_TEMPORARIO_EXCLUIR", (object) dynamicParameters, "AcessoTemporario.ExcluirAcessoTemporario", CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro ExcluirAcessoTemporario() ID Controladora: " + v_id_equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
      }
      finally
      {
        this.Terminate();
      }
    }

    public bool BuscarAcessoTemporario(
      string v_CD_CREDENCIAL,
      string v_s_Aplicacao,
      int v_id_equipamento)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("V_CD_CREDENCIAL", (object) v_CD_CREDENCIAL, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<AcessoTemporario> acessoTemporarios = this.Pesquisar<AcessoTemporario>("BANCO", "SP_ACESSO_TEMPORARIO_BUSCAR", "AcessoTemporario.BuscarAcessoTemporario", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (acessoTemporarios != null)
        {
          using (IEnumerator<AcessoTemporario> enumerator = acessoTemporarios.GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current.CD_CREDENCIAL != null;
          }
        }
        return false;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro BuscarAcessoTemporario() ID Controladora: " + v_id_equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return false;
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

    void IDisposable.Dispose()
    {
      this.Dispose(true);
    }

    public void Terminate()
    {
      this.Dispose(true);
    }
  }
}
