// Decompiled with JetBrains decompiler
// Type: Comum.AjustaDataHora
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using System;
using System.Diagnostics;
using System.Net;

namespace Comum
{
  public class AjustaDataHora : IDisposable
  {
    private bool disposedValue = false;

    public void AjustarDataHora(
      string v_s_Aplicacao,
      int v_Id_Equipamento,
      IPAddress v_IP,
      int v_Porta_Envio)
    {
      try
      {
        DateTime now = DateTime.Now;
        int num = now.Day;
        string str1;
        if (num.ToString().Length != 1)
        {
          now = DateTime.Now;
          num = now.Day;
          str1 = num.ToString();
        }
        else
        {
          now = DateTime.Now;
          num = now.Day;
          str1 = "0" + num.ToString();
        }
        string str2 = str1;
        now = DateTime.Now;
        num = now.Month;
        string str3;
        if (num.ToString().Length != 1)
        {
          now = DateTime.Now;
          num = now.Month;
          str3 = num.ToString();
        }
        else
        {
          now = DateTime.Now;
          num = now.Month;
          str3 = "0" + num.ToString();
        }
        string str4 = str2 + str3;
        now = DateTime.Now;
        num = now.Year;
        string str5 = num.ToString().Substring(2, 2);
        string str6 = str4 + str5;
        now = DateTime.Now;
        string str7;
        if (now.DayOfWeek.ToString("d") == "0")
        {
          str7 = str6 + "07";
        }
        else
        {
          string str8 = str6;
          now = DateTime.Now;
          string str9;
          if (now.DayOfWeek.ToString("d").Length != 1)
          {
            now = DateTime.Now;
            str9 = now.DayOfWeek.ToString("d");
          }
          else
          {
            now = DateTime.Now;
            str9 = "0" + now.DayOfWeek.ToString("d");
          }
          str7 = str8 + str9;
        }
        string str10 = str7;
        now = DateTime.Now;
        num = now.Hour;
        string str11;
        if (num.ToString().Length != 1)
        {
          now = DateTime.Now;
          num = now.Hour;
          str11 = num.ToString();
        }
        else
        {
          now = DateTime.Now;
          num = now.Hour;
          str11 = "0" + num.ToString();
        }
        string str12 = str10 + str11;
        now = DateTime.Now;
        num = now.Minute;
        string str13;
        if (num.ToString().Length != 1)
        {
          now = DateTime.Now;
          num = now.Minute;
          str13 = num.ToString();
        }
        else
        {
          now = DateTime.Now;
          num = now.Minute;
          str13 = "0" + num.ToString();
        }
        string str14 = str12 + str13;
        now = DateTime.Now;
        num = now.Second;
        string str15;
        if (num.ToString().Length != 1)
        {
          now = DateTime.Now;
          num = now.Second;
          str15 = num.ToString();
        }
        else
        {
          now = DateTime.Now;
          num = now.Second;
          str15 = "0" + num.ToString();
        }
        new Enviar().IEnviar("$TST" + (str14 + str15) + "-00#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro AjustarDataHora() Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);
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

    ~AjustaDataHora()
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
