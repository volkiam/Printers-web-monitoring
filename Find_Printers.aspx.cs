using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using SnmpSharpNet;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm5 : System.Web.UI.Page
    {


        public static string SNMP_get(string ip, string oid1)
        {
            string snmp_hostname = "";
            try
            {

                OctetString community = new OctetString("public");
                AgentParameters param = new AgentParameters(community);
                param.Version = SnmpVersion.Ver1;
                IpAddress agent = new IpAddress(ip);//IP address

                UdpTarget target = new UdpTarget((System.Net.IPAddress)agent, 161, 2000, 1);

                Pdu pdu = new Pdu(PduType.Get);
                pdu.VbList.Add(oid1); //Hostname
                SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);

                if (result != null)
                {
                    if (result.Pdu.ErrorStatus != 0)
                    {
                    }
                    else
                    {
                        snmp_hostname = (result.Pdu.VbList[0].Value.ToString().Trim());//Hostname  

                    }
                }
                target.Close();
            }
            catch (Exception)
            { }
            return snmp_hostname;
        }

        public static bool check_prn(string ip)
        {
            bool b = false;
            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(@strConnString);
            string cmd_prn = "";
            cmd_prn = "SELECT * FROM spprinter Where [IP]='" + ip.TrimEnd() + "'";
            DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn, false, false);
            if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count > 0))
            {
                b = true;
            }
            else
            {
                b = false;
            }

            return b;
        }

        string ipadrr;

        public void find_prn()
        {
            int i1, i2, i3, i4;
            int j = 0;

            System.Data.DataTable DTBL_MAIN = new DataTable("MyTable");

            for (i1 = Convert.ToInt32(TextBox1.Text); i1 <= Convert.ToInt32(TextBox5.Text); i1++)
            {
                for (i2 = Convert.ToInt32(TextBox2.Text); i2 <= Convert.ToInt32(TextBox6.Text); i2++)
                {
                    for (i3 = Convert.ToInt32(TextBox3.Text); i3 <= Convert.ToInt32(TextBox7.Text); i3++)
                    {
                        for (i4 = Convert.ToInt32(TextBox4.Text); i4 <= Convert.ToInt32(TextBox8.Text); i4++)
                        {
                            ipadrr = i1.ToString() + "." + i2.ToString() + "." + i3.ToString() + "." + i4.ToString();
                            Label2.Text = "Проверяется IP: " + ipadrr;
                            if (!check_prn(ipadrr))
                            {
                                var timeout = 120;
                                var buffer = new byte[] { 0, 0, 0, 0 };

                                // создаем и отправляем ICMP request
                                var ping = new Ping();
                                var reply = ping.Send(ipadrr, timeout, buffer, new PingOptions { Ttl = 30 });

                                // если ответ успешен
                                if (reply.Status == IPStatus.Success)
                                {
                                    string snmp_hostname = SNMP_get(ipadrr, "1.3.6.1.2.1.1.5.0");
                                    string snmp_model = SNMP_get(ipadrr, "1.3.6.1.2.1.25.3.2.1.3.1");
                                    if (snmp_hostname != "")
                                    {
                                        j = j + 1;
                                        TableRow newrow = new TableRow();
                                        TableCell newcell = new TableCell();
                                        CheckBox cb = new CheckBox();
                                        cb.ID = j.ToString();
                                        cb.AutoPostBack = true;
                                        newcell.Text = "";
                                        newcell.Controls.Add(cb);
                                        newrow.Cells.Add(newcell);
                                        TableCell newcell2 = new TableCell();
                                        newcell2.Text = snmp_hostname;
                                        TableCell newcell3 = new TableCell();
                                        newcell3.Text = ipadrr;
                                        TableCell newcell4 = new TableCell();
                                        newcell4.Text = snmp_model;
                                        newrow.Cells.Add(newcell);
                                        newrow.Cells.Add(newcell2);
                                        newrow.Cells.Add(newcell3);
                                        newrow.Cells.Add(newcell4);
                                        Table1.Rows.Add(newrow);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            Label2.Visible = false;
            //   Timer1.Enabled=false;
            ipadrr = "";
            if (j > 0)
            {
                Button2.Visible = true;
                Table1.DataBind();
                ViewState["table1"] = true;
            }
            else
            {
                Button2.Visible = false;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label2.Visible = true;
            // Timer1.Enabled=true;
            Button2.Visible = false;
            Table1.Rows.Clear();
            find_prn();
        }

        protected override object SaveViewState()
        {
            object[] newViewState = new object[3];

            List<string> txtValues = new List<string>();

            //foreach (TableRow row in Table1.Controls)
            for (int j = 0; j < Table1.Rows.Count; j++)
            {
                // foreach (TableCell cell in row.Controls)
                if (((CheckBox)Table1.Rows[j].Cells[0].Controls[0]).Checked)
                {
                    txtValues.Add("true");
                }
                else
                {
                    txtValues.Add("false");
                }
                for (int i = 1; i < Table1.Rows[j].Cells.Count; i++)
                {
                    txtValues.Add(Table1.Rows[j].Cells[i].Text);
                }
            }

            newViewState[0] = txtValues.ToArray();
            newViewState[1] = Table1.Rows.Count;
            newViewState[2] = base.SaveViewState();
            return newViewState;
        }

        protected override void LoadViewState(object savedState)
        {
            //if we can identify the custom view state as defined in the override for SaveViewState
            if (savedState is object[] && ((object[])savedState).Length == 3 && ((object[])savedState)[0] is string[])
            {
                object[] newViewState = (object[])savedState;
                string[] txtValues = (string[])(newViewState[0]);
                int nom = (int)(newViewState[1]);
                if (txtValues.Length > 0)
                {
                    //re-load tables
                    //CreateDynamicTable();
                    //foreach (TableRow row in Table1.Controls)
                    int j = 0;
                    int i = 0;
                    while (i < nom)
                    {
                        TableRow newrow = new TableRow();
                        TableCell newcell = new TableCell();
                        CheckBox cb = new CheckBox();
                        cb.ID = j.ToString();
                        cb.AutoPostBack = true;
                        string Cb_check = txtValues[j++].ToString();
                        if (Cb_check == "true")
                        {
                            cb.Checked = true;
                        }
                        else
                        {
                            cb.Checked = false;
                        }
                        newcell.Text = "";
                        newcell.Controls.Add(cb);
                        newrow.Cells.Add(newcell);
                        TableCell newcell2 = new TableCell();
                        newcell2.Text = txtValues[j++].ToString();
                        TableCell newcell3 = new TableCell();
                        newcell3.Text = txtValues[j++].ToString();
                        TableCell newcell4 = new TableCell();
                        newcell4.Text = txtValues[j++].ToString();
                        newrow.Cells.Add(newcell);
                        newrow.Cells.Add(newcell2);
                        newrow.Cells.Add(newcell3);
                        newrow.Cells.Add(newcell4);
                        Table1.Rows.Add(newrow);
                        i++;

                    }
                }
                //load the ViewState normally
                base.LoadViewState(newViewState[2]);
            }
            else
            {
                base.LoadViewState(savedState);
            }
        }

        protected void Page_load(object sender, EventArgs e)
        {

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            SaveViewState();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < Table1.Rows.Count; i++)
            {
                int t3 = Table1.Rows[i].Cells[0].Controls.Count;
                Control temp = null;
                if (t3 > 0)
                {
                    temp = Table1.Rows[i].Cells[0].Controls[0];
                }

                int k = i * 4;
                if (temp != null && ((CheckBox)temp).ID == k.ToString())
                {
                    //Separated into 2 if statements for debugging purposes  
                    //ID is correct, but .Checked is always false (even if all of the   boxes are checked)  
                    if (((CheckBox)temp).Checked == true)
                    {
                        var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                        string strConnString = conString.ConnectionString;
                        SqlConnection sqlConn = new SqlConnection(strConnString);
                        SqlCommand sqlComm = new SqlCommand();
                        sqlComm = sqlConn.CreateCommand();
                        sqlComm.CommandText = @"INSERT INTO spprinter (Код, Имя_Принтера,Группа_принтера,IP, Модель,Дата_добавления,Время_добавления) VALUES (@Код, @Имя_Принтера,@Группа_принтера,@IP, @Модель,@Дата_добавления,@Время_добавления)";
                        sqlComm.Parameters.Add("@Код", SqlDbType.Int);
                        Random rnd = new Random();
                        int number = rnd.Next(350, 10000);
                        sqlComm.Parameters["@Код"].Value = number;
                        sqlComm.Parameters.Add("@Имя_Принтера", SqlDbType.VarChar);
                        sqlComm.Parameters["@Имя_Принтера"].Value = Table1.Rows[i].Cells[1].Text;
                        sqlComm.Parameters.Add("@Группа_принтера", SqlDbType.VarChar);
                        sqlComm.Parameters["@Группа_принтера"].Value = "unknown";
                        sqlComm.Parameters.Add("@IP", SqlDbType.VarChar);
                        sqlComm.Parameters["@IP"].Value = Table1.Rows[i].Cells[2].Text;
                        sqlComm.Parameters.Add("@Модель", SqlDbType.VarChar);
                        sqlComm.Parameters["@Модель"].Value = Table1.Rows[i].Cells[3].Text;
                        sqlComm.Parameters.Add("@Дата_добавления", SqlDbType.Date);
                        DateTime curDate = DateTime.Now.Date;
                        sqlComm.Parameters["@Дата_добавления"].Value = curDate.ToShortDateString();
                        sqlComm.Parameters.Add("@Время_добавления", SqlDbType.Time);
                        sqlComm.Parameters["@Время_добавления"].Value = DateTime.Now.TimeOfDay;
                        sqlConn.Open();
                        sqlComm.ExecuteNonQuery();
                        sqlConn.Close();
                        // TableRow delrow = Table1.Rows[i];
                        // Table1.Rows.Remove(delrow);
                    }
                }
            }
            Table1.Rows.Clear();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ipadrr != "")
                Label2.Text = "Проверяется IP: " + ipadrr;

            Label3.Text = DateTime.Now.ToLongTimeString();
        }

    }
}