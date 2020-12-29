// Decompiled with JetBrains decompiler
// Type: Comum.GravaEventLog
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Comum
{
  public class GravaEventLog : IDisposable
  {
    private bool disposedValue = false;

    public void GravarEventLog(
      string v_s_Aplicacao,
      string v_s_Mensagem,
      EventLogEntryType v_Type,
      Exception ex = null)
    {
      try
      {
        string str = "";
        if (ex != null)
          str = new Regex("(linha|line)\\b\\s+\\d+").Match(ex.StackTrace.ToString()).Value;
        if (!EventLog.SourceExists(v_s_Aplicacao))
          EventLog.CreateEventSource(v_s_Aplicacao, v_s_Aplicacao);
        EventLog.WriteEntry(v_s_Aplicacao, "|" + v_s_Mensagem + " - " + str, v_Type);
      }
      catch (Exception ex1)
      {
        string message = ex1.Message;
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

    ~GravaEventLog()
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
