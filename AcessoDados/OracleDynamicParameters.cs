// Decompiled with JetBrains decompiler
// Type: AcessoDados.OracleDynamicParameters
// Assembly: AcessoDados, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38E57E28-5BC2-4C48-8C3E-FC9C9DF02C32
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\G01CNC01\AcessoDados.dll

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;

namespace AcessoDados
{
  public class OracleDynamicParameters : SqlMapper.IDynamicParameters
  {
    private static Dictionary<SqlMapper.Identity, Action<IDbCommand, object>> paramReaderCache = new Dictionary<SqlMapper.Identity, Action<IDbCommand, object>>();
    private Dictionary<string, OracleDynamicParameters.ParamInfo> parameters = new Dictionary<string, OracleDynamicParameters.ParamInfo>();
    private List<object> templates;

    public OracleDynamicParameters()
    {
    }

    public OracleDynamicParameters(object template)
    {
      this.AddDynamicParams(template);
    }

    public void AddDynamicParams(object param)
    {
      object obj = param;
      if (obj == null)
        return;
      OracleDynamicParameters dynamicParameters = obj as OracleDynamicParameters;
      if (dynamicParameters == null)
      {
        IEnumerable<KeyValuePair<string, object>> keyValuePairs = obj as IEnumerable<KeyValuePair<string, object>>;
        if (keyValuePairs == null)
        {
          this.templates = this.templates ?? new List<object>();
          this.templates.Add(obj);
        }
        else
        {
          foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
            this.Add(keyValuePair.Key, keyValuePair.Value, new OracleType?(), new ParameterDirection?(), new int?());
        }
      }
      else
      {
        if (dynamicParameters.parameters != null)
        {
          foreach (KeyValuePair<string, OracleDynamicParameters.ParamInfo> parameter in dynamicParameters.parameters)
            this.parameters.Add(parameter.Key, parameter.Value);
        }
        if (dynamicParameters.templates == null)
          return;
        this.templates = this.templates ?? new List<object>();
        foreach (object template in dynamicParameters.templates)
          this.templates.Add(template);
      }
    }

    public void Add(
      string name,
      object value = null,
      OracleType? dbType = null,
      ParameterDirection? direction = null,
      int? size = null)
    {
      Dictionary<string, OracleDynamicParameters.ParamInfo> parameters = this.parameters;
      string index = OracleDynamicParameters.Clean(name);
      OracleDynamicParameters.ParamInfo paramInfo = new OracleDynamicParameters.ParamInfo();
      paramInfo.Name = name;
      paramInfo.Value = value;
      ParameterDirection? nullable = direction;
      paramInfo.ParameterDirection = nullable.HasValue ? nullable.GetValueOrDefault() : ParameterDirection.Input;
      paramInfo.DbType = dbType;
      paramInfo.Size = size;
      parameters[index] = paramInfo;
    }

    private static string Clean(string name)
    {
      if (!string.IsNullOrEmpty(name))
      {
        switch (name[0])
        {
          case ':':
          case '?':
          case '@':
            return name.Substring(1);
        }
      }
      return name;
    }

    void SqlMapper.IDynamicParameters.AddParameters(
      IDbCommand command,
      SqlMapper.Identity identity)
    {
      this.AddParameters(command, identity);
    }

    protected void AddParameters(IDbCommand command, SqlMapper.Identity identity)
    {
      if (this.templates != null)
      {
        foreach (object template in this.templates)
        {
          SqlMapper.Identity index = identity.ForDynamicParameters(template.GetType());
          Action<IDbCommand, object> paramInfoGenerator;
          lock (OracleDynamicParameters.paramReaderCache)
          {
            if (!OracleDynamicParameters.paramReaderCache.TryGetValue(index, out paramInfoGenerator))
            {
              paramInfoGenerator = SqlMapper.CreateParamInfoGenerator(index, false, false);
              OracleDynamicParameters.paramReaderCache[index] = paramInfoGenerator;
            }
          }
          paramInfoGenerator(command, template);
        }
      }
      foreach (OracleDynamicParameters.ParamInfo paramInfo in this.parameters.Values)
      {
        string index = OracleDynamicParameters.Clean(paramInfo.Name);
        int num1 = !((OracleCommand) command).Parameters.Contains(index) ? 1 : 0;
        OracleParameter parameter;
        if (num1 != 0)
        {
          parameter = ((OracleCommand) command).CreateParameter();
          parameter.ParameterName = index;
        }
        else
          parameter = ((OracleCommand) command).Parameters[index];
        object obj = paramInfo.Value;
        parameter.Value = obj ?? (object) DBNull.Value;
        parameter.Direction = paramInfo.ParameterDirection;
        string str = obj as string;
        if (str != null && str.Length <= 4000)
          parameter.Size = 4000;
        int? size = paramInfo.Size;
        if (size.HasValue)
        {
          OracleParameter oracleParameter = parameter;
          size = paramInfo.Size;
          int num2 = size.Value;
          oracleParameter.Size = num2;
        }
        OracleType? dbType = paramInfo.DbType;
        if (dbType.HasValue)
        {
          OracleParameter oracleParameter = parameter;
          dbType = paramInfo.DbType;
          int num2 = (int) dbType.Value;
          oracleParameter.OracleType = (OracleType) num2;
        }
        if (num1 != 0)
          command.Parameters.Add((object) parameter);
        paramInfo.AttachedParam = (IDbDataParameter) parameter;
      }
    }

    public IEnumerable<string> ParameterNames
    {
      get
      {
        return this.parameters.Select<KeyValuePair<string, OracleDynamicParameters.ParamInfo>, string>((Func<KeyValuePair<string, OracleDynamicParameters.ParamInfo>, string>) (p => p.Key));
      }
    }

    public T Get<T>(string name)
    {
      object obj = this.parameters[OracleDynamicParameters.Clean(name)].AttachedParam.Value;
      if (obj != DBNull.Value)
        return (T) obj;
      if ((object) default (T) != null)
        throw new ApplicationException("Attempting to cast a DBNull to a non nullable type!");
      return default (T);
    }

    private class ParamInfo
    {
      public string Name { get; set; }

      public object Value { get; set; }

      public ParameterDirection ParameterDirection { get; set; }

      public OracleType? DbType { get; set; }

      public int? Size { get; set; }

      public IDbDataParameter AttachedParam { get; set; }
    }
  }
}
