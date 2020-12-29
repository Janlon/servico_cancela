using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using AMIR_UDP_LOGGER;

namespace Comum
{
  public class Enviar : IDisposable
  {
    private static readonly string v_s_Aplicacao = ConfigurationManager.AppSettings["APLICACAO"].ToString();
    private bool disposedValue = false;

    public void IEnviar(
      string v_Comando,
      IPAddress v_IP,
      int v_Porta_Envio,
      string v_s_Aplicacao,
      int v_Id_Equipamento)
    {
      Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      try
      {
        IPEndPoint ipEndPoint = new IPEndPoint(v_IP, v_Porta_Envio);
        byte[] bytes = Encoding.ASCII.GetBytes(v_Comando);
        socket.SendTo(bytes, (EndPoint) ipEndPoint);

                // LOG START
                try
                {
                    Amir_UDP_Logger_3 udp_logger = new Amir_UDP_Logger_3();
                    string s_log_filename = ConfigurationManager.AppSettings["LOG_FILENAME"].ToString();
                    string s_log_udp_ip = ConfigurationManager.AppSettings["UDP_DEBUG_IP"].ToString();
                    string s_log_udp_port = ConfigurationManager.AppSettings["UDP_DEBUG_PORT"].ToString();
                    int i_log_udp_port = Convert.ToInt32(s_log_udp_port);
                    udp_logger.config(s_log_udp_ip, i_log_udp_port, s_log_filename, v_s_Aplicacao);
                    udp_logger.log("INFO", "UDP_TX", "(" + v_IP.ToString() + ":" + v_Porta_Envio.ToString() + ")\t" + v_Comando);
                }
                catch (Exception e2)
                {
                    string str_exception = e2.ToString();
                }
                // LOG END

      }
      catch (Exception ex)
      {
        new GravaEventLog().GravarEventLog(v_s_Aplicacao, "Erro IEnviar() Serviço Controladora: " + v_Id_Equipamento.ToString() + " - " + v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, ex);

                try
                {
                    Amir_UDP_Logger_3 udp_logger = new Amir_UDP_Logger_3();
                    string s_log_filename = ConfigurationManager.AppSettings["LOG_FILENAME"].ToString();
                    string s_log_udp_ip = ConfigurationManager.AppSettings["UDP_DEBUG_IP"].ToString();
                    string s_log_udp_port = ConfigurationManager.AppSettings["UDP_DEBUG_PORT"].ToString();
                    int i_log_udp_port = Convert.ToInt32(s_log_udp_port);
                    udp_logger.config(s_log_udp_ip, i_log_udp_port, s_log_filename, v_s_Aplicacao);
                    udp_logger.log("ERROR", "UDP_TX", "Falha ao enviar '" + v_Comando + "'");
                }
                catch (Exception e2)
                {
                    string str_exception = e2.ToString();
                }
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

    ~Enviar()
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
