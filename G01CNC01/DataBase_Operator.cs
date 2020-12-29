using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//using System.Drawing;
using System.IO;
//using System.Windows.Forms;


using System.Data; // DataTable
using System.Data.SqlClient; // SqlConnection

using System.Globalization; // CultureInfo.InvariantCulture); 
//using Bad_LAP_Remover;


namespace DATABASE_OPERATOR_cs
{
    public class DATABASE_OPERATOR
    {
        // GOLDEN UNIT - Funciona na rede interna da Vopak (LAN)
        public const string ConnectionString_Vopak_ByName = "Data Source=BRALE1S018\\PROD;Initial Catalog=LOCKTEK;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";
        //___BKP___  string ConnectionString_Vopak_ByName = "Data Source=BRALE1S018\\PROD;Initial Catalog=LOCKTEK;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";

        //  Funciona na rede interna da Vopak (LAN)
        public const string ConnectionString_Vopak_ByIP_INT = "Data Source=172.100.0.50\\PROD;Initial Catalog=LOCKTEK;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";

        //  Funciona na rede EXTERNA da Vopak (WAN) - DADOS EM ABERTO SEM CRIPTOGRAFIA!
        public const string ConnectionString_Vopak_ByIP_EXT = "Data Source=187.51.36.234\\PROD;Initial Catalog=LOCKTEK;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";

        //public const string ConnectionString_VM =           "Data Source=192.168.15.232\\PROD;Initial Catalog=LOCKTEK;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";
        //public const string ConnectionString_VM2 = "data source = 192.168.15.232\\SQLEXPRESS;initial catalog = LOCKTEK_OLD; trusted_connection=true";



        // ANTIGOS
        // public const string ConnectionString3A = "Data Source=BRALE1S018\\PROD;Initial Catalog=LOCKTEK_TESTE;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";
        // public const string ConnectionString3a = "Data Source=BRALE1S018\\PROD;Initial Catalog=LOCKTEK_teste;Persist Security Info=True;User ID=LOCKTEK;Password=LockTek@2017;MultipleActiveResultSets=True";
        // public const string ConnectionString2 = "Data Source=WIN-PSM7H672I77\\SQLEXPRESS;Initial Catalog=LOCKTEK;Persist Security Info=True;User ID=locktek;Password=LockTek@2017;;MultipleActiveResultSets=True";

        // Configuaração realmente utilizada
        public string ConnectionString = ConnectionString_Vopak_ByIP_EXT;
        //public string ConnectionString = ConnectionString_VM2;
        //public string ConnectionString = ConnectionString_Vopak_ByIP_INT;



        public class OCR_entry
        {
            public int IdOCR;
            public int IdLocal;
            public string TextoPlacaCarmen;
            public string TextoConfirmado;
            //Score	
            public string str_DataCadastro;
            public string str_DataAlterado;
            public string OBS;

            public DateTime DataCadastro;
            public DateTime DataAlterado;
        }


        public string get_tb_capturaOcr_ByDate(int IdLocal1, int IdLocal2, string DtInicio, string DtFinal, ref List<OCR_entry> dest, out string result)
        {
            string s = "";
            try
            {
                dest.Clear();

                DateTime start = DateTime.UtcNow;
                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DtCon.Open();
                DateTime end = DateTime.UtcNow;
                //TimeSpan timeDiff = end - start;
                s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                string wsql = "";
                wsql += "select * from tb_capturaOcr "; // tb_XXX_capturaOcr
                wsql += " where  datacadastro >= '" + DtInicio + "' and datacadastro <= '" + DtFinal + "'";

                if (IdLocal1 > 0)
                {
                    if (IdLocal2 > 0)
                    {
                        wsql += " and IDLOCAL in (" + IdLocal1.ToString() + "," +IdLocal1.ToString() + ") ";
                    }
                    else
                    {
                        wsql += " and IDLOCAL = " + IdLocal1.ToString();
                    }
                }
                wsql += " and pesagem >0 ";


                // wsql += " and IDLOCAL = " + IdLocal.ToString();
                //wsql += " order by datacadastro";
                wsql += " order by IdOCR";

                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql, DtCon);
                conLap.Fill(u);


                for (int r = 0; r < u.Rows.Count; r++)
                {

                    OCR_entry temp = new OCR_entry();

                    temp.IdOCR = Convert.ToInt32(u.Rows[r][0]);
                    temp.IdLocal = Convert.ToInt32(u.Rows[r][1]);
                    temp.TextoPlacaCarmen = u.Rows[r][2].ToString();
                    temp.TextoConfirmado = u.Rows[r][3].ToString();
                    // score = u.Rows[r][4]
                    temp.str_DataCadastro = u.Rows[r][5].ToString();
                    temp.str_DataAlterado = u.Rows[r][6].ToString();
                    // Pesagem = u.Rows[r][7]

                    temp.DataCadastro = DateTime.ParseExact(temp.str_DataCadastro, "d/M/yyyy H:m:s", CultureInfo.InvariantCulture); ;
                    temp.DataAlterado = DateTime.ParseExact(temp.str_DataAlterado, "d/M/yyyy H:m:s", CultureInfo.InvariantCulture); ;

                    dest.Add(temp);

                    // string str_line = "";
                    // for (int c = 0; c < u.Columns.Count; c++)
                    // {
                    //     string cell_val = u.Rows[r][c].ToString();
                    //     str_line += cell_val + "\t";
                    // }
                    // str_line += "\r\n";
                    // s += str_line;
                }
                // s += "---------------------------------------------\r\n";

                result = s;
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }





        public string get_tb_capturaOcr_ByDate(int IdLocal, string DtInicio, string DtFinal, out string result)
        {
            string s = "";
            try
            {
                DateTime start = DateTime.UtcNow;
                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DtCon.Open();
                DateTime end = DateTime.UtcNow;
                //TimeSpan timeDiff = end - start;
                s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                string wsql = "";
                wsql += "select top 500 * from tb_capturaOcr "; // tb_XXX_capturaOcr
                wsql += " where  datacadastro >= '" + DtInicio + "' and datacadastro <= '" + DtFinal + "'";
                wsql += " and IDLOCAL = " + IdLocal.ToString();
                wsql += " order by datacadastro";

                // 32546	44			0	14/03/2019 12:15:51	14/03/2019 12:15:51	2	

                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql, DtCon);
                conLap.Fill(u);


                for (int r = 0; r < u.Rows.Count; r++)
                {
                    string str_line = "";
                    for (int c = 0; c < u.Columns.Count; c++)
                    {
                        string cell_val = u.Rows[r][c].ToString();
                        str_line += cell_val + "\t";
                    }
                    str_line += "\r\n";
                    s += str_line;
                }
                s += "---------------------------------------------\r\n";

                //MessageBox.Show("fim");
                //return s;
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
            result = s;
            return "";
        }




        public string get_tb_capturaOcr_Info(int id_ocr, out string result) 
        {
            string s = "";
            try
            {
                DateTime start = DateTime.UtcNow;
                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DtCon.Open();
                DateTime end = DateTime.UtcNow;
                // s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                string wsql = "";
                wsql += "select * from tb_capturaOcr "; // tb_XXX_capturaOcr
                wsql += " where IdOCR = '" + id_ocr.ToString() + "' ";

                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql, DtCon);
                conLap.Fill(u);

                for (int r = 0; r < u.Rows.Count; r++)
                {
                    string str_line = "";
                    for (int c = 0; c < u.Columns.Count; c++)
                    {
                        string cell_val = u.Rows[r][c].ToString();
                        str_line += cell_val + "\t";
                    }
                    str_line += "\r\n";
                    s += str_line;
                }
                // s += "---------------------------------------------\r\n";
                result = s;
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }


        

        public string get_tb_capturaOcr_PlacaEsperada(int id_ocr, out string result)
        {
            result = "";
            //amir string s = "";
            try
            {
                DateTime start = DateTime.UtcNow;
                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DtCon.Open();
                DateTime end = DateTime.UtcNow;
                // s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                string wsql = "";
                wsql += "select TextoConfirmado from tb_capturaOcr "; // tb_XXX_capturaOcr
                wsql += " where IdOCR = '" + id_ocr.ToString() + "' ";
                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql, DtCon);
                conLap.Fill(u);
                if ((u.Rows.Count > 0) && (u.Columns.Count > 0))
                {
                    result = u.Rows[0][0].ToString();
                }
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }






        public string Atualiza__TB_CapturaOCR__para__TB_CapturaOCR_2(out string result)
        {
            string s = "";
            try
            {
                DateTime start = DateTime.UtcNow;
                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DateTime end = DateTime.UtcNow;
                s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                DtCon.Open();

                int last_idocr_from_TB_CapturaOcr2 = 0;

                string wsql = "select top 1 IdOCR from TB_CapturaOcr_2 order by IdOCR desc"; // tb_XXX_capturaOcr
                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql, DtCon);
                conLap.Fill(u);
                if ((u.Rows.Count > 0) && (u.Columns.Count > 0))
                {
                    string s1 = u.Rows[0][0].ToString();
                    last_idocr_from_TB_CapturaOcr2 = Convert.ToInt32(s1);
                }

                wsql = "";
                wsql += " insert into TB_CapturaOCR_2 "; // tb_XXX_capturaOcr
                wsql += " (IdOCR, IdLocal, TextoPlacaCarmen, TextoConfirmado, Score, DataCadastro, DataAlterado, Pesagem) "; // , carmem0_jidosha1, ordem_servico, qualidade_ordem_servico
                wsql += " select ";
                wsql += " IdOCR, IdLocal, TextoPlacaCarmen, TextoConfirmado, Score, DataCadastro, DataAlterado, Pesagem "; // , carmem0_jidosha1, ordem_servico, qualidade_ordem_servico
                wsql += " from TB_CapturaOCR ";
                wsql += " where IdOCR > '" + last_idocr_from_TB_CapturaOcr2.ToString() + "' ";
                //
                SqlCommand sx = new SqlCommand(wsql, DtCon);
                SqlDataReader dr1 = sx.ExecuteReader();
                // todo fixme - check result
                dr1.Close();
                //using (SqlDataReader reader = sx.ExecuteReader())
                //{
                //    //while (reader.Read()) { string ss = String.Format("{0}, {1}", reader[0], reader[1]); }
                //}

                DtCon.Close();
                result = s;
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }


        public string put_TB_CapturaImagemOCR(int IdOCR, string filename, string nova_placa, out string result)
        {
            string s = "";
            try
            {
                DateTime start = DateTime.UtcNow;

                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DateTime end = DateTime.UtcNow;
                //TimeSpan timeDiff = end - start;
                s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                string wsql1 = "";
                string wsql2 = "";

                DtCon.Open();


                wsql1 += " update tb_capturaOcr_2 set "; // tb_XXX_capturaOcr
                wsql1 += " TextoPlacaCarmen = '" + nova_placa + "', ";
                wsql1 += " carmem0_jidosha1 = '1' ";
                wsql1 += " where IdOCR = '" + IdOCR.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(wsql1, DtCon);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                // todo fixme - check result
                dr1.Close();


                bool ja_tem_imagem = false;
                // Verifica se ja tem imagem com este IdOCR
                wsql1 = " select IdOCR from TB_CapturaImagemOCR_2 where IdOCR = '" + IdOCR.ToString() + "'"; // TB_XXX_CapturaImagemOCR
                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql1, DtCon);
                conLap.Fill(u);
                if ((u.Rows.Count > 0) && (u.Columns.Count > 0))
                {
                    ja_tem_imagem = true;
                    string xx = u.Rows[0][0].ToString();
                }

                // cruft
                //SqlCommand cmd2 = new SqlCommand(wsql1, DtCon);
                //SqlDataReader dr2 = cmd1.ExecuteReader();
                //// todo fixme - check result
                //dr2.Close();


                // cruft - apagar nao funciona bem...
                //  wsql1 = " delete from TB_CapturaImagemOCR_2 where IdOCR = '" + IdOCR.ToString() + "'"; // TB_XXX_CapturaImagemOCR
                //  SqlCommand cmd2 = new SqlCommand(wsql1, DtCon);
                //  SqlDataReader dr2 = cmd1.ExecuteReader();
                //  // todo fixme - check result
                //  dr2.Close();





                using (var command = DtCon.CreateCommand())
                {
                    byte[] imgBytes = File.ReadAllBytes(filename);

                    if (ja_tem_imagem)
                    {
                        wsql2 = "update TB_CapturaImagemOCR_2 set Image=(@image) where IdOCR = '" + IdOCR.ToString() + "'"; // TB_XXX_CapturaImagemOCR

                    }
                    else
                    {
                        // [IdCapturaImagemOcr] [int] PRIMARY KEY NOT NULL,
                        // [IdOCR] [int] NOT NULL,
                        // [Image] [varbinary] (max) NOT NULL,
                        //
                        wsql2 = "insert into TB_CapturaImagemOCR_2 "; // TB_XXX_CapturaImagemOCR
                        wsql2 += " (IdCapturaImagemOcr, IdOCR, Image) ";
                        wsql2 += " VALUES ";
                        wsql2 += " (";
                        wsql2 += "'" + IdOCR.ToString() + "'";
                        wsql2 += ", ";
                        wsql2 += "'" + IdOCR.ToString() + "'";
                        wsql2 += ", ";
                        wsql2 += "@image";
                        //wsql2 += ", ";
                        wsql2 += " )";
                    }


                    command.CommandText = wsql2;
                    IDataParameter par = command.CreateParameter();
                    par.ParameterName = "image";
                    par.DbType = DbType.Binary;
                    par.Value = imgBytes;
                    command.Parameters.Add(par);
                    command.ExecuteNonQuery();
                }

                // Do Ze:
                // byte[] imgBytes = File.ReadAllBytes(filename);
                // wsql2 = string.Format("update TB_CapturaImagemOCR set Image=(@image) where IdOCR = '{0}'", IdOCR);
                // using (SqlCommand command = DtCon.CreateCommand())
                // {
                //     command.CommandText = wsql2;
                //     IDataParameter par = command.CreateParameter();
                //     par.ParameterName = "image";
                //     par.DbType = DbType.Binary;
                //     par.Value = imgBytes;
                //     command.Parameters.Add(par);
                //     command.ExecuteNonQuery();
                // }

                DtCon.Close();
               
                result = s;
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }







        public string encontra_ordem_servico(int IdOCR, string filename, string nova_placa, out string result)
        {
            string s = "";
            try
            {

                /*
                --DECLARE @PX VARCHAR (8) 
                --SET @PX = 'AQY2799';
                --select top 200 * from TB_CapturaOCR          where DataCadastro >= '2019/09/11 00:00:00' and DataCadastro <= '2019/09/11 23:59:59' and TextoConfirmado = @PX order by IdOCR;
                --select top 100 OrdemServico from TB_Acesso              where DtAcesso     >= '2019/09/11 00:00:00' and DtAcesso     <= '2019/09/11 23:59:59' AND Placa           = @PX order by DtAcesso;
                --select top 100 OrdemServico from TB_MotivacaoTemporaria where DTCadastro   >= '2019/09/11 00:00:00' and DtCadastro   <= '2019/09/11 23:59:59' AND Placa           = @PX order by DtCadastro;
                --select top 100 OrdemServico from TB_Motivacao           where Placa = @PX;

                --select top 100 * from TB_AcessoPatio
                --select top 100 * from TB_AgendaVisita where OrdemServico is not NULL
                ----select top 100 * from TB_MotivacaoTemporariaNaoAutorizada 

                --select top 1000 * from TB_INTERVENCAO3
                --where 
                    --DT_INTERVENCAO_GUARDA >= '2019/09/11 00:00:00' and DT_INTERVENCAO_GUARDA <= '2019/09/11 23:59:59' 
                    --and 
                    --CD_PLACA_VEICULO =  'IWB6823' 

                --11/09/2019 09:40:01	IWB6823	IWB6823
                --11/09/2019 09:41:47	IWB6823	IWB6823
                --11/09/2019 20:24:32	HIM2273	HIM2273
                --11/09/2019 21:30:46	AQY2799	AQY2799
                */

                DateTime start = DateTime.UtcNow;

                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString; //  ConnectionString_Vopak;
                DateTime end = DateTime.UtcNow;
                //TimeSpan timeDiff = end - start;
                s += "tConnect_ms = " + Convert.ToInt32((end - start).TotalMilliseconds).ToString() + "\r\n";

                string wsql1 = "";
                //amir string wsql2 = "";

                DtCon.Open();


                wsql1 += " update tb_capturaOcr_2 set "; // tb_XXX_capturaOcr
                wsql1 += " TextoPlacaCarmen = '" + nova_placa + "', ";
                wsql1 += " carmem0_jidosha1 = '1' ";
                wsql1 += " where IdOCR = '" + IdOCR.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(wsql1, DtCon);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                // todo fixme - check result
                dr1.Close();


                //amir bool ja_tem_imagem = false;
                // Verifica se ja tem imagem com este IdOCR
                wsql1 = " select IdOCR from TB_CapturaImagemOCR_2 where IdOCR = '" + IdOCR.ToString() + "'"; // TB_XXX_CapturaImagemOCR
                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(wsql1, DtCon);
                conLap.Fill(u);
                if ((u.Rows.Count > 0) && (u.Columns.Count > 0))
                {
                    //amir ja_tem_imagem = true;
                    string xx = u.Rows[0][0].ToString();
                }

                DtCon.Close();

                result = s;
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }




        public string generic_sql_query(string query, out string result)
        {
            string s = "";
            try
            {
                DateTime start = DateTime.UtcNow;
                SqlConnection DtCon = new SqlConnection();
                DtCon.ConnectionString = ConnectionString;
                DtCon.Open();
                DateTime end = DateTime.UtcNow;

                DataTable u = new DataTable();
                SqlDataAdapter conLap = new SqlDataAdapter(query, DtCon);
                conLap.Fill(u);

                for (int r = 0; r < u.Rows.Count; r++)
                {
                    string str_line = "";
                    for (int c = 0; c < u.Columns.Count; c++)
                    {
                        string cell_val = u.Rows[r][c].ToString();
                        str_line += cell_val + "\t";
                    }
                    str_line += "\r\n";
                    s += str_line;
                }
                result = s;
                return "";
            }
            catch (Exception e)
            {
                result = e.ToString();
                return e.ToString();
            }
        }




    }
}


