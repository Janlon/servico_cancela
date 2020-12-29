// Decompiled with JetBrains decompiler
// Type: Comum.InicializaControladora
// Assembly: Comum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 99DD0288-5D71-4E3E-8396-2C9790D917B3
// Assembly location: E:\OneDrive\____Sphera_Vopak\servicos_4IP\recriado_DotPeek\A4_Comum_G01CNC01.dll

using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;

namespace Comum
{
  public class InicializaControladora : IDisposable
  {
    private bool disposedValue = false;

    public void Inicializa_Controladora(
      IPAddress v_IP,
      int v_Porta_Envio,
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      try
      {
        string str1 = "";
        string str2 = ((int) Convert.ToInt16(ConfigurationManager.AppSettings["GATE"].Replace("G", ""))).ToString("X").PadLeft(2, '0');
        if (v_s_Aplicacao.Substring(3, 3) == "CNC")
        {
          if (ConfigurationManager.AppSettings["SENTIDO_ACESSO"].ToString() == "E")
            str1 = !(ConfigurationManager.AppSettings["MODO"].ToString() == "B") ? "$S" + str2 + "00EPNEPNEPNNN81580020A100#" : "$S" + str2 + "00EBNEBNEBNNN81580020A100#";
          if (ConfigurationManager.AppSettings["SENTIDO_ACESSO"].ToString() == "S")
            str1 = !(ConfigurationManager.AppSettings["MODO"].ToString() == "B") ? "$S" + str2 + "00SPNSPNSPNNN81580020A100#" : "$S" + str2 + "00SBNSBNSBNNN81580020A100#";
        }
        else
          str1 = !(v_s_Aplicacao.Substring(3, 3) == "POR") ? (!(ConfigurationManager.AppSettings["MODO"].ToString() == "B") ? "$S" + str2 + "00EPNSPNVPNNT81580020A100#" : "$S" + str2 + "00EBNSBNVBNNT81580020A100#") : (!(ConfigurationManager.AppSettings["MODO"].ToString() == "B") ? "$S" + str2 + "00EPNEPNEPNNN81580020A100#" : "$S" + str2 + "00EBNEBNEBNNN81580020A100#");
        if (str1 != "" && str1.Length == 30)
        {
          new Enviar().IEnviar(str1, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
          new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 74, "", str1, (string) null, "E", nameof (Inicializa_Controladora));
        }
        else
          new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 76, "", str1, (string) null, "E", nameof (Inicializa_Controladora));
        new Enviar().IEnviar("$DSM  ***SPHERA***   ***SECURITY*** 005#", v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        string str3 = "1";
        string str4 = "$LIN5#";
        new Enviar().IEnviar(str4, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 56, "", str4, (string) null, "E", nameof (Inicializa_Controladora));
        string str5 = "$CVD0#";
        new Enviar().IEnviar(str5, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 58, "", str5, (string) null, "E", nameof (Inicializa_Controladora));
        string str6 = "$CVV0#";
        new Enviar().IEnviar(str6, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 60, "", str6, (string) null, "E", nameof (Inicializa_Controladora));
        string str7 = "$CVT0#";
        new Enviar().IEnviar(str7, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 62, "", str7, (string) null, "E", nameof (Inicializa_Controladora));
        string str8 = "$CVO0#";
        new Enviar().IEnviar(str8, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 64, "", str8, (string) null, "E", nameof (Inicializa_Controladora));
        string str9 = "$CES" + str3 + "#";
        new Enviar().IEnviar(str9, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 66, "", str9, (string) null, "E", nameof (Inicializa_Controladora));
        string str10 = "$CVL1#";
        new Enviar().IEnviar(str10, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 68, "", str10, (string) null, "E", nameof (Inicializa_Controladora));
        string str11 = "$CEC1#";
        new Enviar().IEnviar(str11, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 14, "", str11, (string) null, "E", nameof (Inicializa_Controladora));
        string str12 = "$CLO030000600002000#";
        new Enviar().IEnviar(str12, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 16, "", str12, (string) null, "E", nameof (Inicializa_Controladora));
        string str13 = "$ISM00900#";
        new Enviar().IEnviar(str13, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 70, "", str13, (string) null, "E", nameof (Inicializa_Controladora));
        string str14 = "$HKS120#";
        new Enviar().IEnviar(str14, v_IP, v_Porta_Envio, v_s_Aplicacao, v_Id_Equipamento);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 72, "", str14, (string) null, "E", nameof (Inicializa_Controladora));
        new AjustaDataHora().AjustarDataHora(v_s_Aplicacao, v_Id_Equipamento, v_IP, v_Porta_Envio);
        new Logar().ILogar(v_s_Aplicacao.ToString(), "0", v_Id_Equipamento, 72, "", str14, (string) null, "E", nameof (Inicializa_Controladora));
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Inicializa Controladora: ok", EventLogEntryType.Information, (Exception) null);
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

    ~InicializaControladora()
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
