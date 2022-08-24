using System.Data;
using System.Data.SqlClient;
using System;
using System.Globalization;
using System.IO;

namespace WebApplication1
{     
    //Класс для работы с БД≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
    public class SQL_MANAGER
    {

        public SQL_MANAGER(string _connString)
        {
            connString = _connString;
        }
        string connString = "";

        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataTable SQL_G_Processor(string cmd, bool Exec, bool Return_Scope, ref bool Transfert_Result)
        {
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            Transfert_Result = false;
            SqlConnection SQLConn = null;
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.CommandTimeout = 0;
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (Exec)
                {
                    //Вернуть идентифкатор измененной строки---------------------------------------
                    if (Return_Scope)
                    {
                        SQLCMD.CommandText += " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ";
                        SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                        DataTable DTBL_RESULT = new DataTable();
                        SQLDta.Fill(DTBL_RESULT);
                        SQLDta.Dispose();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L))/1000000;
                        //test(f, L, cmd);
                        Transfert_Result = true;
                        return DTBL_RESULT;
                    }
                    else
                    {
                        SQLCMD.ExecuteNonQuery();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L))/1000000;
                        //test(f, L, cmd);
                        Transfert_Result = true;
                        return null;
                    }
                }
                else
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataTable DTBL_RESULT = new DataTable();
                    SQLDta.Fill(DTBL_RESULT);
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L)) / 1000000;
                    //test(f, L, cmd);                   
                    Transfert_Result = true;
                    return DTBL_RESULT;
                }
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString(), "SQL");
                Transfert_Result = false;
                return null;
            }
        }


        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataTable SQL_G_Processor(SqlCommand SQLCMD, bool Exec, bool Return_Scope, ref bool Transact_Result)
        {
            Transact_Result = false;
            SqlConnection SQLConn = null;
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (Exec)
                {
                    //Вернуть идентифкатор измененной строки---------------------------------------
                    if (Return_Scope)
                    {
                        SQLCMD.CommandText += " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ";
                        SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                        DataTable DTBL_RESULT = new DataTable();
                        SQLDta.Fill(DTBL_RESULT);
                        SQLDta.Dispose();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        Transact_Result = true;
                        return DTBL_RESULT;
                    }
                    else
                    {
                        SQLCMD.ExecuteNonQuery();
                        SQLConn.Close();
                        Transact_Result = true;
                        return null;
                    }
                }
                else
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataTable DTBL_RESULT = new DataTable();
                    SQLDta.Fill(DTBL_RESULT);
                    SQLConn.Close();
                    Transact_Result = true;
                    return DTBL_RESULT;
                }
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString(), "SQL");
                Transact_Result = false;
                return null;
            }
        }


        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataTable SQL_SELECTOR(SqlCommand SQLCMD, bool Exec, bool Return_Scope)
        {
            SQLCMD.CommandText = TextOperator(SQLCMD.CommandText, null, -1);
            if (SQLCMD.CommandText.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = null;
            if ((SQLCMD == null) || (SQLCMD.CommandText.Length == 0)) { return null; }
             try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (Exec)
                {
                    //Вернуть идентифкатор измененной строки---------------------------------------
                    if (Return_Scope)
                    {
                        SQLCMD.CommandText += " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ";
                        SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                        DataTable DTBL_RESULT = new DataTable();
                        SQLDta.Fill(DTBL_RESULT);
                        SQLDta.Dispose();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        return DTBL_RESULT;
                    }
                    else
                    {
                        SQLCMD.ExecuteNonQuery();
                        SQLConn.Close();
                        return null;
                    }
                }
                else
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataTable DTBL_RESULT = new DataTable();
                    SQLDta.Fill(DTBL_RESULT);
                    SQLConn.Close();
                    return DTBL_RESULT;
                }
            }
             catch (Exception Ex) { 
                 //ASM.ACTUNG_13("(" + SQLCMD.CommandText + ") " + Ex.ToString()); 
                 return null; 
             }
        }

        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataTable SQL_SELECTOR(string cmd, bool Exec, bool Return_Scope)
        {
            //double L = DateTime.Now.ToFileTime();
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = null;
            if ((cmd == null) || (cmd.Length == 0)) { return null; }
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (Exec)
                {
                    //Вернуть идентифкатор измененной строки---------------------------------------
                    if (Return_Scope)
                    {
                        SQLCMD.CommandText += " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ";
                        SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                        DataTable DTBL_RESULT = new DataTable();
                        SQLDta.Fill(DTBL_RESULT);
                        SQLDta.Dispose();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L))/1000000;
                        //test(f, L, cmd);
                        return DTBL_RESULT;
                    }
                    else
                    {
                        SQLCMD.ExecuteNonQuery();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L))/1000000;
                        //test(f, L, cmd);
                        return null;
                    }
                }
                else
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataTable DTBL_RESULT = new DataTable();
                    SQLDta.Fill(DTBL_RESULT);
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L)) / 1000000;
                    //test(f, L, cmd);                   
                    return DTBL_RESULT;
                }
            }
            catch (Exception Ex) 
            {
                //System.Windows.Forms.MessageBox.Show(Ex.ToString());
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null; 
            }
        }

        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataTable SQL_SELECTOR(string cmd, bool Exec, bool Return_Scope, int timeout)
        {
            //double L = DateTime.Now.ToFileTime();
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = null;
            if ((cmd == null) || (cmd.Length == 0)) { return null; }
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.Connection = SQLConn;
                SQLCMD.CommandTimeout = timeout;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (Exec)
                {
                    //Вернуть идентифкатор измененной строки---------------------------------------
                    if (Return_Scope)
                    {
                        SQLCMD.CommandText += " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ";
                        SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                        DataTable DTBL_RESULT = new DataTable();
                        SQLDta.Fill(DTBL_RESULT);
                        SQLDta.Dispose();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L))/1000000;
                        //test(f, L, cmd);
                        return DTBL_RESULT;
                    }
                    else
                    {
                        SQLCMD.ExecuteNonQuery();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L))/1000000;
                        //test(f, L, cmd);
                        return null;
                    }
                }
                else
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataTable DTBL_RESULT = new DataTable();
                    SQLDta.Fill(DTBL_RESULT);
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    //double f = Convert.ToDouble((DateTime.Now.ToFileTime()) - Convert.ToDouble(L)) / 1000000;
                    //test(f, L, cmd);                   
                    return DTBL_RESULT;
                }
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }


        private void test(double f, double L,  string cmd)
        {
            long Lg = DateTime.Now.ToFileTime();

          string path = "c:\\test\\!__.txt";
                    //if (!File.Exists(path))
                    //{
                    //    using (StreamWriter sw = File.CreateText(path))
                    //    {
                    //        sw.WriteLine(L.ToString() + "-" + f.ToString() + Environment.NewLine);
                    //    }
                    //}
                    //else
                    //{
          if (f >= 1)
          {
              using (StreamWriter sw = File.AppendText(path))
              {
                  sw.WriteLine(Lg.ToString() + "-" + f.ToString());
              }
              System.IO.TextWriter writeFile = new StreamWriter("c:\\test\\" + Lg.ToString() + ".txt");
              writeFile.WriteLine(cmd);
              writeFile.Flush();
              writeFile.Close();
              writeFile = null;
          }
        }

        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataSet DTS_SQL_SELECTOR(string cmd, bool Exec, bool Return_Scope)
        {
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = null;
            if ((cmd == null) || (cmd.Length == 0)) { return null; }
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (Exec)
                {
                    //Вернуть идентифкатор измененной строки---------------------------------------
                    if (Return_Scope)
                    {
                        SQLCMD.CommandText += " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ";
                        SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                        DataSet DTS_TMP = new DataSet();
                        SQLDta.Fill(DTS_TMP);
                        SQLDta.Dispose();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        return DTS_TMP;
                    }
                    else
                    {
                        SQLCMD.ExecuteNonQuery();
                        SQLCMD.Dispose();
                        SQLConn.Close();
                        SQLConn.Dispose();
                        return null;
                    }
                }
                else
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataSet DTS_TMP = new DataSet();
                    SQLDta.Fill(DTS_TMP);
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    return DTS_TMP;
                }
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }

        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataSet DTS_SQL_SELECTOR(string cmd)
        {
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = null;
            if ((cmd == null) || (cmd.Length == 0)) { return null; }
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (cmd.IndexOf("SELECT") > -1)
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataSet DTS_RESULT = new DataSet();
                    SQLDta.Fill(DTS_RESULT);
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    return DTS_RESULT;
                }
                if (((cmd.IndexOf("INSERT") > -1) || (cmd.IndexOf("UPDATE") > -1) || (cmd.IndexOf("DELETE") > -1)))
                {
                    SQLCMD.ExecuteNonQuery();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    return null;
                }
                return null;
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }

        //Механизм выгрузки содержимого из БД≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public byte[] SQL_SELECTOR(string cmd)
        {
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = new SqlConnection(connString);
            if ((cmd == null) || (cmd.Length == 0)) { return null; }
            try
            {
                byte[] StackData = null;
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.Connection = SQLConn;
                DataTable DTBL = new DataTable();
                SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                SQLDta.Fill(DTBL);
                if (DTBL.Rows.Count >= 1)
                {
                    SqlDataReader Reader = SQLCMD.ExecuteReader();
                    Reader.Read();
                    {
                        StackData = (byte[])Reader[0];
                    }
                    Reader.Close();
                    Reader.Dispose();
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    return StackData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }

        //Механизм исполнения SQL команд≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public DataTable SQL_SELECTOR(string cmd, bool dummy)
        {
            cmd = TextOperator(cmd, null, -1);
            if (cmd.IndexOf("Err") > -1) { return null; }
            SqlConnection SQLConn = null;
            if ((cmd == null) || (cmd.Length == 0)) { return null; }
            try
            {
                SQLConn = new SqlConnection(connString);
                SQLConn.Open();
                SqlCommand SQLCMD = new SqlCommand(cmd);
                SQLCMD.Connection = SQLConn;
                //Исполнить код или сделать выборку(Exec)------------------------------------------
                if (cmd.IndexOf("SELECT") > -1)
                {
                    SqlDataAdapter SQLDta = new SqlDataAdapter(SQLCMD);
                    DataTable DTBL_RESULT = new DataTable();
                    SQLDta.Fill(DTBL_RESULT);
                    SQLDta.Dispose();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    return DTBL_RESULT;
                }
                if (((cmd.IndexOf("INSERT") > -1) || (cmd.IndexOf("UPDATE") > -1) || (cmd.IndexOf("DELETE") > -1)))
                {
                    SQLCMD.ExecuteNonQuery();
                    SQLCMD.Dispose();
                    SQLConn.Close();
                    SQLConn.Dispose();
                    return null;
                }
                return null;
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }

        //Механизм замены переменных в SQL запросах≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public string TextOperator(string str_SQL_QUERY, DataTable Dtbl_Param, int indexOfFirstParam)
        {
            try
            {
                bool stop = false;
                if (Dtbl_Param != null)
                {
                    //Ищем кодовый символ "@" - это символ подмены переменной------------------------------
                    string SymbolForSearch = "@P";
                    int int_IndexOf = str_SQL_QUERY.IndexOf(SymbolForSearch);
                    if (int_IndexOf > -1)
                    {

                        int int_numFirstParam = 1;
                        //Пока еще остались кодовые символы продожаем менять их на значения переменных-
                        while (stop != true)
                        {
                            string s = Dtbl_Param.Rows[indexOfFirstParam][1].ToString();
                            //Заменяем "болванку" значением из таблицы---------------------------------
                            str_SQL_QUERY = str_SQL_QUERY.Replace(SymbolForSearch + int_numFirstParam.ToString(), Dtbl_Param.Rows[indexOfFirstParam][1].ToString());
                            indexOfFirstParam++;
                            int_numFirstParam++;
                            //Если параметров больше 10 тогда параметры нумеруются @PA10 сделано для того чтобы параметр @P1 не заменил еще и @P10 так как первые 3 символа у них одинаковые
                            if (int_numFirstParam > 9)
                            {
                                SymbolForSearch = "@PA";
                            }
                            //Проверяем есть ли следующий параметр-------------------------------------
                            int_IndexOf = str_SQL_QUERY.IndexOf(SymbolForSearch + int_numFirstParam);
                            if (int_IndexOf == -1)
                            {
                                stop = true;
                            }
                        }
                    }
                }
                //Ищем кодовый символ "{" - это символ спецкода для спецсценария
                stop = false;
                while (stop != true)
                {
                    //Ищем команду---------------------------------------------------------------------
                    //Например команда {DTR} означает что нужно вставить текущее время итд-------------
                    int StartPos;//начальная позиция
                    int LengthWord;//длинна слова
                    string CMD_PARAM = "";//Параметр
                    //Ищем начало команды--------------------------------------------------------------
                    string SymbolForSearch = "{";
                    int int_IndexOf = str_SQL_QUERY.IndexOf(SymbolForSearch);
                    if (int_IndexOf > -1)
                    {
                        StartPos = int_IndexOf + 1;
                        //Ищем конец команды-----------------------------------------------------------
                        LengthWord = (str_SQL_QUERY.IndexOf("}") - 1) - (int_IndexOf);
                        string CMD_STRINGS = str_SQL_QUERY.Substring(StartPos, LengthWord);
                        string OLD_CMD_STRINGS = CMD_STRINGS;
                        //Ищем параметры---------------------------------------------------------------
                        //Комманды можно передавать с параметрами например {КОМАННДА(ПАРАМЕТР)}
                        SymbolForSearch = "(";
                        int_IndexOf = CMD_STRINGS.IndexOf(SymbolForSearch);
                        if (int_IndexOf > -1)
                        {
                            StartPos = int_IndexOf + 1;
                            LengthWord = (CMD_STRINGS.IndexOf(")") - 1) - (int_IndexOf);
                            CMD_PARAM = CMD_STRINGS.Substring(StartPos, LengthWord);
                            CMD_STRINGS = CMD_STRINGS.Substring(0, StartPos - 1);
                        }
                        //Определяем что это за команда--------------------------------------------
                        switch (CMD_STRINGS)
                        {
                            //SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]--------------------------
                            case "SEL_ID":
                                {
                                    str_SQL_QUERY = str_SQL_QUERY.Replace("{" + OLD_CMD_STRINGS + "}", "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY] ");
                                }
                                break;
                            //Получить текущий номер дела------------------------------------------
                            case "GET_CURR_DO_NUM":
                                {
                                   // DOCUMENT_MANAGER DM = new DOCUMENT_MANAGER();
                                  //  str_SQL_QUERY = str_SQL_QUERY.Replace("{" + OLD_CMD_STRINGS + "}", DM.GET_CURR_DO_NUM(CMD_PARAM));
                                }
                                break;
                            //Текущее дата + время в формате FileTime------------------------------
                            case "DTR":
                                {
                                    str_SQL_QUERY = str_SQL_QUERY.Replace("{" + OLD_CMD_STRINGS + "}", DateTime.Now.ToFileTime().ToString());
                                }
                                break;
                            //Единая система датировки---------------------------------------------
                            case "DT":
                                {
                                    string result = "Err";
                                    if (CMD_PARAM.Length > 0)
                                    {
                                        string[] S = CMD_PARAM.Split('-');
                                        if (S.Length == 6)
                                        {
                                            //string result_date = S[0] + "/" + S[1] + "/" + S[2] + " " + S[3] + ":" + S[4] + ":" + S[5];
                                            //DateTime DT = DateTime.Parse(result_date, new CultureInfo("ru-RU", false));
                                            //result = DT.ToFileTime().ToString();

                                            DateTime D = new DateTime(1, 1, 1, 0, 0, 0);
                                            D = D.AddYears(Convert.ToInt32(S[2]) - 1);
                                            D = D.AddMonths(Convert.ToInt32(S[1]) - 1);
                                            D = D.AddDays(Convert.ToInt32(S[0]) - 1);
                                            D = D.AddHours(Convert.ToInt32(S[3]));
                                            D = D.AddMinutes(Convert.ToInt32(S[4]));
                                            D = D.AddMilliseconds(Convert.ToInt32(S[5]));
                                            result = D.ToFileTime().ToString();
                                        }
                                        str_SQL_QUERY = str_SQL_QUERY.Replace("{" + OLD_CMD_STRINGS + "}", result);
                                    }

                                }
                                break;
                           
                        }
                    }
                    else { stop = true; }
                }
                return str_SQL_QUERY;
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }

        //Механизм получения номера документа≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public string GET_NUMBER_OF_TICKET()
        {
            try
            {
                //1-Выбираем текущий номер---------------------------------------------------------
                string cmd = "SELECT VALUE_SETTING FROM CURRENT_SETTINGS WHERE NAME_SETTING = 'NUM_TICKET'";
                string NUM_TICKET = (Convert.ToInt32(SQL_SELECTOR(cmd, false, false).Rows[0]["VALUE_SETTING"]) + 1).ToString();
                //2-Обновляем запись о крайнем номере---------------------------------------------- 
                cmd = "UPDATE CURRENT_SETTINGS SET VALUE_SETTING = '" + NUM_TICKET + "'  WHERE NAME_SETTING = 'NUM_TICKET'";
                SQL_SELECTOR(cmd, true, false);
                return NUM_TICKET;
            }
            catch (Exception Ex)
            {
                //ASM.ACTUNG_13("(" + cmd + ") " + Ex.ToString()); 
                return null;
            }
        }

        //Механизм возвращающий строку в кавычках≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public string QuotedString(string Input)
        {
            return "'" + Input + "'";
        }

        //Механизм возвращающий слепок текущего времени≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡
        public string DTR()
        {
            return DateTime.Now.ToFileTime().ToString();
        }
    
    }
}
