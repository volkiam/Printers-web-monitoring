using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetGroup_to_List();
                }
            }
            catch
            {
            }
        }

        string errtrace = "1"; 

        private void GetGroup_to_List()
        {
            try
            {
                var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                string strConnString = conString.ConnectionString;
                SQL_MANAGER SM_ListPRN = new SQL_MANAGER(@strConnString);
                errtrace = "2"; 
                if (DDL_main==null) DDL_main.Items.Clear();
                errtrace = "3"; 
                DDL_main.Items.Add("Все");
                errtrace = "4"; 
                string cmd_prn2 = "SELECT * FROM ListOfGroup";
                errtrace = "5"; 
                DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
                errtrace = "6";
                int k = 7;
                if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count > 1))
                {
                errtrace = "7";
                    for (int i = 0; i < DTBL_ListPRN.Rows.Count; i++)
                    {
                        k = k + i;
                        errtrace = k.ToString();
                        DDL_main.Items.Add(DTBL_ListPRN.Rows[i][1].ToString());
                    }
                }
                
            }
            catch
            {
                Label1.Visible = true;
                Label1.Text ="Ошибка в строке "+ errtrace;
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
           try
            {
                 // Create a new DataTable.
                System.Data.DataTable DTBL_MAIN = new DataTable("MyTable");
                // Create new DataColumn, set DataType, 
                // ColumnName and add to DataTable.  

                DataColumn workCol = DTBL_MAIN.Columns.Add("Имя принтера", typeof(String));
                workCol.AllowDBNull = false;
                workCol.Unique = false;

                DTBL_MAIN.Columns.Add("Группа", typeof(String));
                DTBL_MAIN.Columns.Add("IP", typeof(String));
                DTBL_MAIN.Columns.Add("Модель", typeof(String));
                DTBL_MAIN.Columns.Add("Количество страниц", typeof(Int32));

                var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                string strConnString = conString.ConnectionString;
                SQL_MANAGER SM_AURORA = new SQL_MANAGER(@strConnString);

                String grp = DDL_main.Text;
                
                string cmd_prn = "";
                if (grp == "Все")
                {
                    cmd_prn = "SELECT * FROM spprinter";
                }
                else
                {
                    cmd_prn = "SELECT * FROM spprinter Where [Группа_принтера]='" + grp.TrimEnd() + "'";
                }

                DataTable DTBL_PRN = SM_AURORA.SQL_SELECTOR(cmd_prn, false, false);
                for (int i = 0; i < DTBL_PRN.Rows.Count-1; i++) // 
                {
                    object ip = DTBL_PRN.Rows[i][3];
                    string find_date_from = Calendar1.SelectedDate.ToShortDateString();
                   // string date_from = find_date_from.ToShortDateString();
                    string find_date_to = Calendar2.SelectedDate.ToShortDateString();
                    string cmd_from = "SELECT  TOP(1) * FROM PRINTERS WHERE IP like '" + ip.ToString().Trim() + "' AND ДатаВремя_проверки >='" + find_date_from.Trim() + "  00:01:00' ORDER BY ДатаВремя_проверки"; 
                        //"SELECT * FROM (TOP(1) * FROM PRINTERS WHERE IP like '" + ip.ToString().Trim() + "' AND Дата_проверки >= '" + find_date_from.ToString().Trim() + "'ORDER BY Дата_проверки)a ORDER BY Время_проверки";
                    DataTable DTBL_From = SM_AURORA.SQL_SELECTOR(cmd_from, false, false);


                    string cmd_to = "SELECT  TOP(1) * FROM PRINTERS WHERE IP like '" + ip.ToString().Trim() + "' AND ДатаВремя_проверки <='" + find_date_to.Trim() + "  23:59:59' ORDER BY ДатаВремя_проверки DESC";
                    DataTable DTBL_TO = SM_AURORA.SQL_SELECTOR(cmd_to, false, false);

                    //if (i == 30) { 
                    //string nnn="wer";
                    //}

                    if ((DTBL_From != null) & (DTBL_TO.Rows.Count >= 1) & (DTBL_From.Rows.Count >= 1))
                    {
                    DataRow row = DTBL_MAIN.NewRow();
                    row["Имя принтера"] = DTBL_From.Rows[0][0];
                    row["Группа"] = DTBL_PRN.Rows[i][2];
                    row["IP"] = DTBL_From.Rows[0][2];
                    row["Модель"] = DTBL_From.Rows[0][1];
                    Int32 kol_from = Convert.ToInt32(DTBL_From.Rows[0][4]);
                    Int32 kol_to = Convert.ToInt32(DTBL_TO.Rows[0][4]);
                    row["Количество страниц"] = kol_to-kol_from;   
                    DTBL_MAIN.Rows.Add(row);
                    }
                }

                GV_main.DataSource = DTBL_MAIN;
                GV_main.DataBind();

                return;
            }
            catch (Exception Ex)
            {
                //System.Window.Forms.MessageBox.Show("Ошибка! Метод: " + System.Reflection.MethodBase.GetCurrentMethod().ToString() + " ErrTrace = " + ErrTrace);
                return;
            }

            //ErrEnd:
            //MessageBox.Show("Ошибка! Метод: " + System.Reflection.MethodBase.GetCurrentMethod().ToString() + " ErrTrace = " + ErrTrace);
            return;

        }
    }
}
