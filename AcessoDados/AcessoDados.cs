// Decompiled with JetBrains decompiler
// Type: AcessoDados.AcessoDados
// Assembly: AcessoDados, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38E57E28-5BC2-4C48-8C3E-FC9C9DF02C32
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\G01CNC01\AcessoDados.dll

using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AcessoDados
{
  public abstract class AcessoDados
  {
    private string TipoBanco = ConfigurationManager.AppSettings["TIPOBANCO"].ToString();
    private string v_s_Aplicacao = ConfigurationManager.AppSettings["APLICACAO"].ToString();

    public string ObterStringConexao(string connectionString)
    {
      return ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
    }

    private IDbConnection ObterConexao(string connectionString)
    {
      try
      {
        IDbConnection dbConnection = (IDbConnection) null;
        if (this.TipoBanco == "SQL")
          dbConnection = (IDbConnection) new SqlConnection(this.ObterStringConexao(connectionString));
        if (this.TipoBanco == "ORACLE")
          dbConnection = (IDbConnection) new OracleConnection(this.ObterStringConexao(connectionString));
        dbConnection.Open();
        return dbConnection;
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_s_Aplicacao))
          EventLog.CreateEventSource(this.v_s_Aplicacao, this.v_s_Aplicacao);
        EventLog.WriteEntry(this.v_s_Aplicacao, "Erro ObterConexão() Acesso a Dados - " + ex.Message, EventLogEntryType.Error);
        return (IDbConnection) null;
      }
    }

    public IEnumerable<T> Pesquisar<T>(
      string query,
      string connectionString,
      string v_Cliente)
    {
      try
      {
        IDbConnection cnn = this.ObterConexao(connectionString);
        IEnumerable<T> objs;
        try
        {
          objs = cnn.Query<T>(query, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?());
        }
        finally
        {
          cnn.Close();
          cnn.Dispose();
        }
        return objs;
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_s_Aplicacao))
          EventLog.CreateEventSource(this.v_s_Aplicacao, this.v_s_Aplicacao);
        EventLog.WriteEntry(this.v_s_Aplicacao, "Erro Pesquisar(QueryString) Acesso a Dados - Cliente: " + v_Cliente + " - " + ex.Message, EventLogEntryType.Error);
        return (IEnumerable<T>) null;
      }
    }

    public IEnumerable<T> Pesquisar<T>(
      string connectionString,
      string consulta,
      string v_Cliente,
      object parametros = null,
      CommandType tipoComando = CommandType.StoredProcedure,
      bool comBuffer = true)
    {
      try
      {
        IDbConnection cnn = this.ObterConexao(connectionString);
        IEnumerable<T> objs;
        try
        {
          objs = cnn.Query<T>(consulta, parametros, (IDbTransaction) null, comBuffer, new int?(), new CommandType?(tipoComando));
        }
        finally
        {
          cnn.Close();
          cnn.Dispose();
        }
        return objs;
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_s_Aplicacao))
          EventLog.CreateEventSource(this.v_s_Aplicacao, this.v_s_Aplicacao);
        EventLog.WriteEntry(this.v_s_Aplicacao, "Erro Pesquisar(Parâmetros) Acesso a Dados - Procedure: " + consulta + " - Cliente: " + v_Cliente + " - " + ex.Message, EventLogEntryType.Error);
        return (IEnumerable<T>) null;
      }
    }

    public void Executar(string query, string connectionString, string v_Cliente)
    {
      try
      {
        IDbConnection cnn = this.ObterConexao(connectionString);
        try
        {
          cnn.Query(query, (object) null, (IDbTransaction) null, true, new int?(), new CommandType?());
        }
        finally
        {
          cnn.Close();
          cnn.Dispose();
        }
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_s_Aplicacao))
          EventLog.CreateEventSource(this.v_s_Aplicacao, this.v_s_Aplicacao);
        EventLog.WriteEntry(this.v_s_Aplicacao, "Erro Executar(QueryString) Acesso a Dados - Cliente: " + v_Cliente + " - " + ex.Message, EventLogEntryType.Error);
      }
    }

    public void Executar(
      string connectionString,
      string procedimento,
      object parametros,
      string v_Cliente,
      CommandType tipoComando = CommandType.Text)
    {
      try
      {
        IDbConnection cnn = this.ObterConexao(connectionString);
        try
        {
          cnn.Query(procedimento, parametros, (IDbTransaction) null, true, new int?(), new CommandType?(tipoComando));
        }
        finally
        {
          cnn.Close();
          cnn.Dispose();
        }
      }
      catch (Exception ex)
      {
        if (!EventLog.SourceExists(this.v_s_Aplicacao))
          EventLog.CreateEventSource(this.v_s_Aplicacao, this.v_s_Aplicacao);
        EventLog.WriteEntry(this.v_s_Aplicacao, "Erro Executar(Parâmetros) Acesso a Dados - Procedure: " + procedimento + " - Cliente: " + v_Cliente + " - " + ex.Message, EventLogEntryType.Error);
      }
    }
  }
}
