// Decompiled with JetBrains decompiler
// Type: Dominio.TipoLog
// Assembly: Dominio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 42FF7EF8-6A0D-4AFA-B801-22F342067CC7
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\servicos\G06TOR01\Dominio.dll

using System.Collections.Generic;

namespace Dominio
{
  public class TipoLog
  {
    public int ID_TIPO_LOG { get; set; }

    public string DESCRICAO { get; set; }

    public IList<Log> Logs { get; set; }

    public TipoLog _ID_TIPO_LOG(int v_ID_TIPO_LOG)
    {
      this.ID_TIPO_LOG = v_ID_TIPO_LOG;
      return this;
    }

    public TipoLog _DESCRICAO(string v_DESCRICAO)
    {
      this.DESCRICAO = v_DESCRICAO;
      return this;
    }

    public TipoLog _Logs(IList<Log> v_Logs)
    {
      this.Logs = v_Logs;
      return this;
    }
  }
}
