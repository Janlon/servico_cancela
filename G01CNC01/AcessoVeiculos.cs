using Comum;
using DLL_Acesso_Cancela;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;

// ######################################################################################################################
//
// ---- Seção do AMIR - INICIO

using AMIR_UDP_LOGGER;

// ---- Seção do AMIR - FIM
//
// ######################################################################################################################

namespace AcessoVeiculos
{
    public class AcessoVeiculos : ServiceBase
    {

        // ######################################################################################################################
        // ######################################################################################################################
        // ######################################################################################################################
        //
        // ---- Seção do AMIR - INICIO

        // ######################################################################################################################
        // ######################################################################################################################
        //
        // Variáveis e Objetos

        public static string send_debug_data_by_UDP__IP = "127.0.0.1"; // --------------------------------------- PARAMETRO CONFIGURAVEL
        public static int send_debug_data_by_UDP__port = 19200; // ---------------------------------------------- PARAMETRO CONFIGURAVEL // this.v_s_Porta_Envio + 2000? // porta envio 4IP = 15048  -->  porta envio DEBUG = 17048
        public static string log_filename = "log.txt"; // ------------------------------------------------------- PARAMETRO CONFIGURAVEL
        public Amir_UDP_Logger_1 udp_logger = new Amir_UDP_Logger_1();
        //
        public bool debug_mode = false;
        public bool bloqueia_envio_LIU = false; // -------------------------------------------------------------- PARAMETRO DEBUG!!! DEVE SER FALSE NA VERSAO FINAL!


        // ######################################################################################################################
        // ######################################################################################################################
        //
        // Funções

        public void amir_debug_breakpointa()
        {
            int i = 0; i++;
        }

        // ######################################################################################################################

        private string try_read_config_string(string par_name, ref string dest, string default_value, ref bool fail_flag)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[par_name].ToString();
                dest = s;
                return ""; // OK
            }
            catch (Exception e)
            {
                string str_exception = e.ToString();
                dest = default_value;
                fail_flag = true;
                return "Falha ao tentar ler parametro '" + par_name + "'\r\n";
            }
        }

        // ######################################################################################################################

        private string try_read_config_int___(string par_name, ref int dest, int default_value, ref bool fail_flag)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[par_name].ToString();
                try
                {
                    int i = Convert.ToInt32(s);
                    dest = i;
                    return ""; // OK
                }
                catch (Exception e)
                {
                    string str_exception = e.ToString();
                    dest = default_value;
                    fail_flag = true;
                    return "Falha ao tentar converter parametro '" + par_name + "' para numero, cujo valor fornecido foi '" + s + "'\r\n";
                }
            }
            catch (Exception e)
            {
                string str_exception = e.ToString();
                dest = default_value;
                fail_flag = true;
                return "Falha ao tentar ler parametro '" + par_name + "'\r\n";
            }
        }

        // ######################################################################################################################

        private string try_read_config_bool__(string par_name, ref bool dest, bool default_value, ref bool fail_flag)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[par_name].ToString().Trim().ToUpper();
                if (s == "0" || s == "FALSE")
                {
                    dest = false;
                    return ""; // OK
                }
                if (s == "1" || s == "TRUE")
                {
                    dest = true;
                    return ""; // OK
                }
                fail_flag = true;
                return "Falha ao tentar converter parametro '" + par_name + "', cujo valor fornecido foi '" + s + "'\r\n";
            }
            catch (Exception e)
            {
                string str_exception = e.ToString();
                dest = default_value;
                fail_flag = true;
                return "Falha ao tentar ler parametro '" + par_name + "'\r\n";
            }
        }

        // ######################################################################################################################

        private string read_config(ref bool critical_fail_flag)
        {
            // tenta ler parametros de configuracao.
            // Alguns podem falhar (passa dummy_fail_flag para ser setado em caso de falha)
            // Outros nao podem falhar (passa real_fail_flag para ser setado em caso de falha)
            // No final, se real_fail_flag estiver setado, aborta execução (?)
            string s_errors = ""; // OK
            bool dummy_fail_flag = false; // If read_config fails, it´s ok to use default value
            bool real_fail_flag = false; // If read_config fails, ABORT!
            try
            {
                // Parâmetros opcionais
                s_errors += try_read_config_bool__("DEBUG_MODE", ref debug_mode, false, ref dummy_fail_flag);
                s_errors += try_read_config_string("LOG_FILENAME", ref log_filename, "log.txt", ref dummy_fail_flag);
                s_errors += try_read_config_string("UDP_DEBUG_IP", ref send_debug_data_by_UDP__IP, "127.0.0.1", ref dummy_fail_flag);
                s_errors += try_read_config_bool__("BLOQUEIA_ENVIO_LIU", ref bloqueia_envio_LIU, false, ref dummy_fail_flag);

                // Parâmetros obrigatórios
                s_errors += try_read_config_int___("ID_EQUIPAMENTO", ref v_Id_Equipamento, 0, ref real_fail_flag);
                s_errors += try_read_config_string("APLICACAO", ref v_s_Aplicacao, "GxxYYYzz", ref real_fail_flag);

            }
            catch (Exception e)
            {
                string str_exception = e.ToString();
                s_errors += "Falha geral em read_config()\r\n";
            }
            critical_fail_flag = real_fail_flag;
            return s_errors;
        }

        // ---- Seção do AMIR - FIM
        //
        // ######################################################################################################################
        // ######################################################################################################################
        // ######################################################################################################################

        private int v_Id_Equipamento = 0; // int.Parse(ConfigurationManager.AppSettings["ID_EQUIPAMENTO"].ToString());
        private string v_s_Aplicacao = ""; // ConfigurationManager.AppSettings["APLICACAO"].ToString();
        private Logar myLogar = new Logar();
        private bool done = false;
        private bool done_ping = false;
        private IContainer components = (IContainer)null;
        private int v_Porta_Escuta;
        private int v_Porta_Envio;
        private IPAddress v_IP;
        private Thread v_t_Receber;
        private Thread v_t_AtualizaLista;
        private Thread v_t_TestaPing;

        public AcessoVeiculos()
        {
            new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Ok", EventLogEntryType.Information, (Exception)null);
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(int.Parse(ConfigurationManager.AppSettings["CPU"].ToString()));
            this.InitializeComponent();
        }





        protected override void OnStart(string[] args)
        {
            bool critical_fail_flag = false; // If read_config fails, ABORT!
            string s_errors = read_config(ref critical_fail_flag);
            if (critical_fail_flag)
            {
                // abort?
            }

            if (debug_mode)
                System.Diagnostics.Debugger.Launch();

            udp_logger.config(send_debug_data_by_UDP__IP, send_debug_data_by_UDP__port, log_filename, v_s_Aplicacao);
            udp_logger.log("INFO", "MAIN", "Started!");

            // Verifica
            if (debug_mode)
            {
                udp_logger.log("INFO", "MAIN", "FYI: bloqueia_envio_LIU esta como " + bloqueia_envio_LIU.ToString());
            }
            else
            {
                if (bloqueia_envio_LIU)
                    udp_logger.log("ERROR", "MAIN", "--- 'bloqueia_envio_LIU' ESTA ATIVO!!! NAO DEVERIA...");
            }

            this.Inicio();
        }

        private void Inicio()
        {
            try
            {
                new CriaChaveRegistroWindows().CriarChaveRegistroWindows(this.v_s_Aplicacao);
                new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - Foi Iniciado", EventLogEntryType.Information, (Exception)null);
                Base.ExecutaTestePing = true;
                PegaControladora pegaControladora = new PegaControladora().PegarControladora(this.v_Id_Equipamento, this.v_s_Aplicacao);
                this.v_IP = IPAddress.Parse(pegaControladora.Ip);
                this.v_Porta_Envio = int.Parse(pegaControladora.NrPorta);
                this.v_Porta_Escuta = int.Parse(pegaControladora.NrPortaSaida);
                pegaControladora.Terminate();
                if ((uint)this.v_Porta_Escuta > 0U)
                {
                    if (this.v_t_Receber != null)
                    {
                        if (this.v_t_Receber.ThreadState == System.Threading.ThreadState.Stopped || this.v_t_Receber.ThreadState == System.Threading.ThreadState.Aborted || this.v_t_Receber.ThreadState == System.Threading.ThreadState.AbortRequested)
                        {
                            this.v_t_Receber = new Thread(new ThreadStart(this.Receber));
                            this.v_t_Receber.Start();
                        }
                    }
                    else
                    {
                        this.v_t_Receber = new Thread(new ThreadStart(this.Receber));
                        this.v_t_Receber.Start();
                    }
                    if (this.v_t_AtualizaLista != null)
                    {
                        if (this.v_t_AtualizaLista.ThreadState == System.Threading.ThreadState.Stopped || this.v_t_AtualizaLista.ThreadState == System.Threading.ThreadState.Aborted || this.v_t_AtualizaLista.ThreadState == System.Threading.ThreadState.AbortRequested)
                        {
                            this.v_t_AtualizaLista = new Thread(new ThreadStart(this.AtualizaLista));
                            this.v_t_AtualizaLista.Start();
                        }
                    }
                    else
                    {
                        this.v_t_AtualizaLista = new Thread(new ThreadStart(this.AtualizaLista));
                        this.v_t_AtualizaLista.Start();
                    }
                    if (this.v_t_TestaPing != null)
                    {
                        if (this.v_t_TestaPing.ThreadState == System.Threading.ThreadState.Stopped || this.v_t_TestaPing.ThreadState == System.Threading.ThreadState.Aborted || this.v_t_TestaPing.ThreadState == System.Threading.ThreadState.AbortRequested)
                        {
                            this.v_t_TestaPing = new Thread(new ThreadStart(this.TestePing));
                            this.v_t_TestaPing.Start();
                        }
                    }
                    else
                    {
                        this.v_t_TestaPing = new Thread(new ThreadStart(this.TestePing));
                        this.v_t_TestaPing.Start();
                    }
                    new FechaFalha().FecharFalha(this.v_s_Aplicacao, this.v_Id_Equipamento);
                }
                else
                    new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro OnStart ID Controladora: " + this.v_Id_Equipamento.ToString() + " - Aplicação não carregou a porta que ela deve escutar, verifique o ID do equipamento e coloque o ID correto no appconfig", EventLogEntryType.Error, (Exception)null);
                udp_logger.log("INFO", "Inicializa_Controladora()", "Ponto 1");
                new InicializaControladora().Inicializa_Controladora(this.v_IP, this.v_Porta_Envio, this.v_s_Aplicacao, this.v_Id_Equipamento);
            }
            catch (Exception ex)
            {
                new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro Inicio() Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + ex.Message, EventLogEntryType.Error, ex);
            }
        }

        protected override void OnStop()
        {
            new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Serviço da Controladora: " + this.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao + " - Foi Encerrado", EventLogEntryType.Warning, (Exception)null);
            this.done = true;
            Thread.Sleep(3000);
            this.v_t_Receber.Abort();
            this.v_t_AtualizaLista.Abort();
            this.v_t_TestaPing.Abort();
            this.Stop();
        }

        public void Receber()
        {
            UdpClient udpClient = new UdpClient(this.v_Porta_Escuta);
            IPEndPoint remoteEP = new IPEndPoint(this.v_IP, this.v_Porta_Escuta);

            // Cria objeto regras
            Regras regras = new Regras();
            udp_logger.log("INFO", "UDP_RX", "FYI: Configurado para 4IP usando IP=" + this.v_IP + "; portas=" + this.v_Porta_Envio.ToString() + "/" + this.v_Porta_Escuta.ToString());

            // CONFIGURA objeto regras
            regras.bloqueia_envio_LIU = bloqueia_envio_LIU;
            regras.udp_logger.config(udp_logger.m_IP, udp_logger.m_port, udp_logger.m_log_filename, udp_logger.m_application_name);


            try
            {
                while (!this.done)
                {
                    try
                    {
                        byte[] bytes = udpClient.Receive(ref remoteEP);
                        string ip = remoteEP.Address.ToString();
                        string v_Comando = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                        if (new PegaConfiguracaoServico().PegarConfiguracaoServico(this.v_s_Aplicacao, ip, this.v_Id_Equipamento).ID_EQUIPAMENTO_TIPO != 0 || v_Comando.Substring(1, 3) == "NTR")
                        {
                            regras.IRegras_MultiIO(v_Comando, this.v_IP, this.v_Porta_Envio);
                        }
                        else
                        {
                            udp_logger.log("INFO", "UDP_RX", "(" + this.v_IP.ToString() + ":" + this.v_Porta_Envio.ToString() + ")\t" + v_Comando);
                            regras.IRegras(v_Comando, this.v_IP, this.v_Porta_Envio);
                        }
                    }
                    catch (Exception ex)
                    {
                        new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro Receber() - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + ex.Message, EventLogEntryType.Error, ex);
                        udp_logger.log("ERRO", "UDP_RX", ex.ToString());

                        if (this.v_t_Receber.ThreadState == System.Threading.ThreadState.Stopped || this.v_t_Receber.ThreadState == System.Threading.ThreadState.Aborted || this.v_t_Receber.ThreadState == System.Threading.ThreadState.AbortRequested)
                        {
                            this.v_t_Receber = new Thread(new ThreadStart(this.Receber));
                            this.v_t_Receber.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro Receber() - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + ex.Message, EventLogEntryType.Error, ex);
            }
            finally
            {
                udpClient.Close();
            }
        }

        public void AtualizaLista()
        {
            try
            {
                Thread.Sleep(20000);
                while (!this.done)
                {
                    if (Base.ContaTestePing == 0)
                        new AtualizaListaControladora().AtualizarListaControladora(this.v_s_Aplicacao, this.v_Id_Equipamento, this.v_IP, this.v_Porta_Envio);
                    Thread.Sleep(60000);
                }
            }
            catch (Exception ex)
            {
                new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro AtualizaLista() - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + ex.Message, EventLogEntryType.Error, ex);
            }
        }

        public void TestePing()
        {
            try
            {
                Base.ContaTestePing = 0;
                DateTime now = DateTime.Now;
                int num = 1;
                while (!this.done_ping)
                {
                    try
                    {
                        Thread.Sleep(5000);
                        if ((now - DateTime.Now).Hours == 1)
                        {
                            now = DateTime.Now;
                            new AjustaDataHora().AjustarDataHora(this.v_s_Aplicacao, this.v_Id_Equipamento, this.v_IP, this.v_Porta_Envio);
                        }
                        if (Base.ExecutaTestePing)
                        {
                            string[] strArray = new TestePing().TestarPing(this.v_IP, this.v_Porta_Envio, this.v_s_Aplicacao, this.v_Id_Equipamento).Split('|');
                            if (strArray[0] == "NOKP" || strArray[1] == "NOKC" || strArray[1] == "NOKE")
                            {
                                if (Base.ContaTestePing == 12)
                                {
                                    new AbreFalha().AbrirFalha(this.v_s_Aplicacao, this.v_Id_Equipamento);
                                    new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "FALHA DE COMUNICAÇÃO 4IP, Teste Ping = " + strArray[0] + " - Teste Retorno de Comando = " + strArray[1] + " - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao, EventLogEntryType.Error, (Exception)null);
                                }
                                ++Base.ContaTestePing;
                            }
                            else
                            {
                                if (Base.ContaTestePing > 6)
                                {
                                    udp_logger.log("INFO", "Inicializa_Controladora()", "Ponto 2");
                                    new InicializaControladora().Inicializa_Controladora(this.v_IP, this.v_Porta_Envio, this.v_s_Aplicacao, this.v_Id_Equipamento);
                                    new Enviar().IEnviar("$RESET#", this.v_IP, this.v_Porta_Envio, this.v_s_Aplicacao, this.v_Id_Equipamento);
                                    new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Comunicação restabelecida - 4IP OK - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao, EventLogEntryType.Information, (Exception)null);
                                }
                                if (Base.ContaTestePing > 12)
                                    new FechaFalha().FecharFalha(this.v_s_Aplicacao, this.v_Id_Equipamento);
                                Base.ContaTestePing = 0;
                                num = 1;
                            }
                        }
                        else
                        {
                            ++num;
                            if (num > 25)
                            {
                                udp_logger.log("INFO", "Inicializa_Controladora()", "Ponto 3");
                                new InicializaControladora().Inicializa_Controladora(this.v_IP, this.v_Porta_Envio, this.v_s_Aplicacao, this.v_Id_Equipamento);
                                new Enviar().IEnviar("$RESET#", this.v_IP, this.v_Porta_Envio, this.v_s_Aplicacao, this.v_Id_Equipamento);
                                new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Teste de Ping Parado, tempo parado excedido, seção encerrada e teste reiniciado  - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao, EventLogEntryType.Warning, (Exception)null);
                                num = 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro TestePing() - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, (Exception)null);
                    }
                }
            }
            catch (Exception ex)
            {
                new GravaEventLog().GravarEventLog(this.v_s_Aplicacao, "Erro TestePing() - Serviço Controladora: " + this.v_Id_Equipamento.ToString() + " - " + this.v_s_Aplicacao + " - Erro: " + ex.Message, EventLogEntryType.Error, (Exception)null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // 
            // AcessoVeiculos
            // 
            this.ServiceName = "Service1";

        }
    }
}
