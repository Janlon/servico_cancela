// Decompiled with JetBrains decompiler
// Type: Comum.VerificaRetornoScore
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
  public class VerificaRetornoScore : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public int QT_REGISTROS { get; set; }

    public bool VerificarRetornoScore(
      long v_IdSecao,
      string v_s_Aplicacao,
      string v_Id_Equipamento)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdSecao", (object) v_IdSecao, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<VerificaRetornoScore> verificaRetornoScores = this.Pesquisar<VerificaRetornoScore>("BANCO", "SP_VERIFICA_RETORNO_SCORE", "VerificaRetornoScore.VerificarRetornoScore", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (verificaRetornoScores != null)
        {
          using (IEnumerator<VerificaRetornoScore> enumerator = verificaRetornoScores.GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current.QT_REGISTROS >= 1;
          }
        }
        return false;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificarRetornoScore(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~VerificaRetornoScore()
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
