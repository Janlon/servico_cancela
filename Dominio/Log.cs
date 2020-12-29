// Decompiled with JetBrains decompiler
// Type: Dominio.Log
// Assembly: Dominio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 42FF7EF8-6A0D-4AFA-B801-22F342067CC7
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\servicos\G06TOR01\Dominio.dll

using System;

namespace Dominio
{
  public class Log
  {
    public long ID_LOG { get; set; }

    public DateTime? DT_CONTROLADORA { get; set; }

    public int ID_TIPO_LOG { get; set; }

    public int ID_MENSAGEM { get; set; }

    public long? ID_SECAO { get; set; }

    public string ID_CREDENCIAL { get; set; }

    public int? ID_EQUIPAMENTO { get; set; }

    public string NM_APLICACAO { get; set; }

    public int ID_CRITICIDADE { get; set; }

    public string DS_MENSAGEM_LIVRE { get; set; }

    public string CD_SENTIDO { get; set; }

    public Log _ID_LOG(long v_ID_LOG)
    {
      this.ID_LOG = v_ID_LOG;
      return this;
    }

    public Log _DT_CONTROLADORA(DateTime? v_DT_CONTROLADORA)
    {
      this.DT_CONTROLADORA = v_DT_CONTROLADORA;
      return this;
    }

    public Log _ID_TIPO_LOG(int v_ID_TIPO_LOG)
    {
      this.ID_TIPO_LOG = v_ID_TIPO_LOG;
      return this;
    }

    public Log _ID_MENSAGEM(int v_ID_MENSAGEM)
    {
      this.ID_MENSAGEM = v_ID_MENSAGEM;
      return this;
    }

    public Log _ID_SECAO(long? v_ID_SECAO)
    {
      this.ID_SECAO = v_ID_SECAO;
      return this;
    }

    public Log _ID_CREDENCIAL(string v_ID_CREDENCIAL)
    {
      this.ID_CREDENCIAL = v_ID_CREDENCIAL;
      return this;
    }

    public Log _ID_EQUIPAMENTO(int? v_ID_EQUIPAMENTO)
    {
      this.ID_EQUIPAMENTO = v_ID_EQUIPAMENTO;
      return this;
    }

    public Log _NM_APLICACAO(string v_NM_APLICACAO)
    {
      this.NM_APLICACAO = v_NM_APLICACAO;
      return this;
    }

    public Log _ID_CRITICIDADE(int v_ID_CRITICIDADE)
    {
      this.ID_CRITICIDADE = v_ID_CRITICIDADE;
      return this;
    }

    public Log _DS_MENSAGEM_LIVRE(string v_DS_MENSAGEM_LIVRE)
    {
      this.DS_MENSAGEM_LIVRE = v_DS_MENSAGEM_LIVRE;
      return this;
    }

    public Log _CD_SENTIDO(string v_CD_SENTIDO)
    {
      this.CD_SENTIDO = v_CD_SENTIDO;
      return this;
    }
  }
}
