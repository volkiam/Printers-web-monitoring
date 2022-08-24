using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {


        private void GetPrinters(string grp)
        {

            System.Data.DataTable DTBL_MAIN = new DataTable("MyTable");
            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.  

            DataColumn workCol = DTBL_MAIN.Columns.Add("Номер", typeof(Int32));
            workCol.AllowDBNull = false;
            workCol.Unique = false;
            DTBL_MAIN.Columns.Add("Имя принтера", typeof(String));
            DTBL_MAIN.Columns.Add("Группа", typeof(String));
            DTBL_MAIN.Columns.Add("IP", typeof(String));
            DTBL_MAIN.Columns.Add("Модель", typeof(String));
            DTBL_MAIN.Columns.Add("Дата добавления", typeof(String));

            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(@strConnString);
            string cmd_prn2 = "";
            if (grp== "Все")
            {
                cmd_prn2 = "SELECT * FROM spprinter";
            }
            else
            {
                cmd_prn2 = "SELECT * FROM spprinter Where [Группа_принтера]='" + grp.TrimEnd() + "'";
            }
            
            DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
            if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count > 0))
            {
                int j = 0;
                for (int i = 0; i < DTBL_ListPRN.Rows.Count; i++)
                {
                    j = j + 1;
                    DataRow row = DTBL_MAIN.NewRow();
                    row["Номер"] = j.ToString();
                    row["Имя принтера"] = DTBL_ListPRN.Rows[i][1];
                    row["Группа"] = DTBL_ListPRN.Rows[i][2];
                    row["IP"] = DTBL_ListPRN.Rows[i][3];
                    row["Модель"] = DTBL_ListPRN.Rows[i][4];
                    row["Дата добавления"] = DTBL_ListPRN.Rows[i][5] + " " + DTBL_ListPRN.Rows[i][6];
                    DTBL_MAIN.Rows.Add(row);
                }
                GV_fulllistprn.DataSource = DTBL_MAIN;
                GV_fulllistprn.DataBind();
            }
            else
            {
                GV_fulllistprn.DataSource = null;
                GV_fulllistprn.DataBind();
            }
        }

        private void GetGroup_to_List()
        {
            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(@strConnString);
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add("Все");
            string cmd_prn2 = "SELECT * FROM ListOfGroup";
            DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
            if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count > 0))
            {
                for (int i = 0; i < DTBL_ListPRN.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(DTBL_ListPRN.Rows[i][1].ToString());
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DropDownList1.SelectedItem == null) GetGroup_to_List();
                GetPrinters("Все");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPrinters(DropDownList1.SelectedValue);
        }
    }
}