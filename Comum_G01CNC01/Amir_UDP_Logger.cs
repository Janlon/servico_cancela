using System;
using System.Text;

using System.Net;
using System.Net.Sockets;  // socket
using System.IO; // Directory.xxx & File.xxx

namespace AMIR_UDP_LOGGER
{
    public class Amir_UDP_Logger_3
    {
        public string m_IP = ""; // 127.0.0.1";
        public int m_port = 0; // 19200; 
        public string m_log_filename = ""; // "log.txt";
        public string m_application_name = ""; // "app";

        // ######################################################################################################################

        public void config(string IP, int port, string log_filename, string applicaton_name)
        {
            m_IP = IP;
            m_port = port;
            m_log_filename = log_filename;
            m_application_name = applicaton_name;
        }

        // ######################################################################################################################

        public int log(string text1, string text2, string text3) // OK0 ERR1
        {
            return log(m_IP, m_port, text1 + "\t" + text2 + "\t" + text3);
        }

        // ######################################################################################################################

        //public int log(string text1, string text2) // OK0 ERR1
        //{
        //    return log(m_IP, m_port, text1 + "\t" + text2);
        //}

        // ######################################################################################################################

        //public int log(string text) // OK0 ERR1
        //{
        //    return log(m_IP, m_port, text);
        //}

        // ######################################################################################################################

        public int log(string IP, int port, string text) // OK0 ERR1
        {
            int err_code = 0; // OK

            DateTime now = DateTime.Now;
            string str_date = now.Year.ToString() + "/" + now.Month.ToString().PadLeft(2, '0') + "/" + now.Day.ToString().PadLeft(2, '0');
            string str_time = now.Hour.ToString().PadLeft(2, '0') + ":" + now.Minute.ToString().PadLeft(2, '0') + ":" + now.Second.ToString().PadLeft(2, '0') + "." + now.Millisecond.ToString().PadLeft(3, '0');

            string s = "";
            s += str_date + " " + str_time + "\t";
            s += m_application_name + "\t";


            // Write to file
            bool wrote_to_file = true;
            try
            {
                if (m_log_filename.Length > 0)
                {
                    using (StreamWriter sw = File.AppendText(m_log_filename))
                    {
                        sw.WriteLine(s + text);
                    }
                }
            }
            catch (Exception e)
            {
                string sx = e.ToString();
                wrote_to_file = false;
                err_code = 1; // ERR
            }

            // Send by UDP
            try
            {
                if (IP.Length > 0 && port > 0)
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPAddress serverAddr = IPAddress.Parse(IP);
                    IPEndPoint endPoint = new IPEndPoint(serverAddr, port);

                    byte[] send_buffer1 = Encoding.ASCII.GetBytes(s + text + "\r\n");
                    sock.SendTo(send_buffer1, endPoint);

                    if (!wrote_to_file)
                    {
                        byte[] send_buffer2 = Encoding.ASCII.GetBytes(s + "Failed to write " + m_log_filename + "\r\n");
                        sock.SendTo(send_buffer2, endPoint);
                    }
                }
            }
            catch (Exception e)
            {
                string sx = e.ToString();
                err_code = 1; // ERR
            }

            return err_code;
        }

    }
}
