// Decompiled with JetBrains decompiler
// Type: Comum.NovaSecao
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
  public class NovaSecao : Base, IDisposable
  {
    private bool disposedValue = false;

    private string v_s_Secao { get; set; }

    public string Secao(string v_s_Aplicacao, int v_Id_Equipamento)
    {
      try
      {
        DynamicParameters dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<NovaSecao> novaSecaos = this.Pesquisar<NovaSecao>("BANCO", "SP_SecaoInserir", "NovaSecao.Secao()", (object) dynamicParameters, CommandType.StoredProcedure, true);
        if (novaSecaos != null)
        {
          using (IEnumerator<NovaSecao> enumerator = novaSecaos.GetEnumerator())
          {
            if (enumerator.MoveNext())
              return enumerator.Current.v_s_Secao;
          }
        }
        return "";
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro Secao(). Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~NovaSecao()
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
