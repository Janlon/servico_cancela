// Decompiled with JetBrains decompiler
// Type: Comum.TestePing
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace Comum
{
  public class TestePing : Base, IDisposable
  {
    private bool disposedValue = false;

    public string TestarPing(
      IPAddress v_Ip,
      int v_Porta,
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      Ping ping = new Ping();
      try
      {
        string str1 = !(ping.Send(v_Ip.ToString(), 5000).Status.ToString() == "Success") ? "NOKP|" : "OK|";
        new Enviar().IEnviar("$_VIVO#", v_Ip, v_Porta, v_s_Aplicacao, v_Id_Equipamento);
        Thread.Sleep(1000);
        string str2;
        if (Base.v_ComandoPing != "")
        {
          Base.v_ComandoPing = "";
          str2 = str1 + "OK";
        }
        else
          str2 = str1 + "NOKC";
        return str2;
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro TestarPing(). ID Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
        return "E|NOKE";
      }
      finally
      {
        ping.Dispose();
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

    ~TestePing()
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
