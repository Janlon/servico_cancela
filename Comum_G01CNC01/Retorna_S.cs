// Decompiled with JetBrains decompiler
// Type: Comum.Retorna_S
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
  public class Retorna_S : AcessoDados.AcessoDados, IDisposable
  {
    private bool disposedValue = false;

    private string COMANDO_S { get; set; }

    public string Retornar_S(string v_s_Aplicacao, int v_Id_Equipamento)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<Retorna_S> retornaSes = this.Pesquisar<Retorna_S>("BANCO", "SP_EQUIP_CONFIG_INICIO", "Retorna_S.Retornar_S", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (retornaSes != null)
        {
          using (IEnumerator<Retorna_S> enumerator = retornaSes.GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              Retorna_S current = enumerator.Current;
              return current.COMANDO_S != null ? current.COMANDO_S : "";
            }
          }
        }
        return "";
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro Configuração(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return "";
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

    ~Retorna_S()
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
