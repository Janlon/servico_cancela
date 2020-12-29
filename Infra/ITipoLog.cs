// Decompiled with JetBrains decompiler
// Type: Infra.ITipoLog
// Assembly: Infra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4930A2D8-6462-4B91-93A8-EA33E1212BC7
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\G01CNC01\Infra.dll

using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Infra
{
  public class ITipoLog : BaseInfra
  {
    public IEnumerable<TipoLog> Listar(
      string connectionString,
      string query,
      object parameters,
      CommandType commandType,
      bool comBuffer,
      string v_Cliente = "")
    {
      try
      {
        return this.Pesquisar<TipoLog>(connectionString, query, v_Cliente, parameters, commandType, comBuffer);
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_Aplicacao))
          EventLog.CreateEventSource(this.v_Aplicacao, this.v_Aplicacao);
        EventLog.WriteEntry(this.v_Aplicacao, "Erro ITipoLog.Listar() Log -  - query: " + query + " - erro: " + ex.Message, EventLogEntryType.Error);
        return (IEnumerable<TipoLog>) null;
      }
    }

    public void Salvar(
      string connectionString,
      string query,
      object parameters,
      CommandType commandType,
      string v_Cliente = "")
    {
      try
      {
        this.Executar(connectionString, query, parameters, v_Cliente, commandType);
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_Aplicacao))
          EventLog.CreateEventSource(this.v_Aplicacao, this.v_Aplicacao);
        EventLog.WriteEntry(this.v_Aplicacao, "Erro ITipoLog.Salvar() Log -  - query: " + query + " - erro: " + ex.Message, EventLogEntryType.Error);
      }
    }
  }
}
