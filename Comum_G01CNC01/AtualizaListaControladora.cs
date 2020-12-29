// Decompiled with JetBrains decompiler
// Type: Comum.AtualizaListaControladora
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

namespace Comum
{
  public class AtualizaListaControladora : Base, IDisposable
  {
    private Logar myLogar = new Logar();
    private bool disposedValue = false;

    public long ID_CARGA_ARQUIVO { get; set; }

    public string CDACAO { get; set; }

    public string STR { get; set; }

    public void AtualizarListaControladora(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      IPAddress v_IP,
      int v_Porta_Envio)
    {
      AtualizaListaControladora listaControladora1 = new AtualizaListaControladora();
      try
      {
        int num1 = 1;
        int num2 = 1;
        Base.v_UltimoComandoLista = "";
        DynamicParameters dynamicParameters1 = new DynamicParameters();
        dynamicParameters1.Add("vIdEquipamento", (object) v_Id_Equipamento, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
        IEnumerable<AtualizaListaControladora> source = this.Pesquisar<AtualizaListaControladora>("BANCO", "SP_ListaBuscar_4IP", "AtualizaListaControladora.AtualizarListaControladora", (object) dynamicParameters1, CommandType.StoredProcedure, true);
        if (source == null || source.Count<AtualizaListaControladora>() <= 0)
          return;
        this.myLogar.ILogar(v_s_Aplicacao.ToString(), "", v_Id_Equipamento, 79, "", "INÍCIO DE ENVIO DA LISTA PARA A CONTROLADORA - " + v_s_Aplicacao.ToString(), (string) null, "", "");
        foreach (AtualizaListaControladora listaControladora2 in source)
        {
          Base.Envio_Lista_Iniciado = true;
          new Enviar().IEnviar("$DSMATUALIZANDO     SISTEMA....     011#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
          if (listaControladora2.CDACAO == "I")
            new Enviar().IEnviar("$LI" + listaControladora2.STR + "#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
          else
            new Enviar().IEnviar("$LE" + listaControladora2.STR + "#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
          while (Base.v_UltimoComandoLista == "")
          {
            ++num2;
            Thread.Sleep(1000);
            if (num2 == 10)
            {
              num2 = 1;
              new Enviar().IEnviar("$DSMATUALIZANDO     SISTEMA....     011#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
              Thread.Sleep(1000);
              new Enviar().IEnviar("$LI" + listaControladora2.STR + "#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
            }
          }
          if (Base.v_UltimoComandoLista.Split('@')[1].Substring(2, 2) == "OK")
          {
            DynamicParameters dynamicParameters2 = new DynamicParameters();
            dynamicParameters2.Add("vId", (object) listaControladora2.ID_CARGA_ARQUIVO, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            this.Executar("BANCO", "SP_MotivacaoEnviada", (object) dynamicParameters2, "GravaAcesso.GravarAcesso()", CommandType.StoredProcedure);
            this.myLogar.ILogar(v_s_Aplicacao.ToString(), "", v_Id_Equipamento, 54, "", "ENVIO ITEM LISTA OK. ID: " + (object) listaControladora2.ID_CARGA_ARQUIVO, (string) null, "", "");
          }
          ++num1;
          Base.v_UltimoComandoLista = "";
          num2 = 1;
        }
        this.myLogar.ILogar(v_s_Aplicacao.ToString(), "", v_Id_Equipamento, 80, "", "FIM DE ENVIO DA LISTA PARA A CONTROLADORA - " + v_s_Aplicacao.ToString(), (string) null, "", "");
        Base.Envio_Lista_Iniciado = false;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro AtualizarListaControladora(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
      }
      finally
      {
        listaControladora1.Terminate();
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

    ~AtualizaListaControladora()
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
