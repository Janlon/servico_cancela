// Decompiled with JetBrains decompiler
// Type: Comum.VerificandoDuploAccesso
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
  public class VerificandoDuploAccesso : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    public string IdCredencial { get; set; }

    public bool VerificandoDuploAcesso(
      string v_credencial,
      string v_cd_sentido,
      int v_id_equipamento,
      string v_s_Aplicacao)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdCredencial", (object) v_credencial, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        dynamicParameters.Add("vCdSentido", (object) v_cd_sentido, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<VerificandoDuploAccesso> verificandoDuploAccessos = this.Pesquisar<VerificandoDuploAccesso>("BANCO", "SP_DuploAcessoVerificando", "VerificandoDuploAccesso.VerificandoDuploAcesso", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (verificandoDuploAccessos != null)
        {
          using (IEnumerator<VerificandoDuploAccesso> enumerator = verificandoDuploAccessos.GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current.IdCredencial != null;
          }
        }
        return false;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro VerificandoDuploAcesso() ID Controladora: " + v_id_equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~VerificandoDuploAccesso()
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
