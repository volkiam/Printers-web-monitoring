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
    
    public partial class WebForm1 : System.Web.UI.Page
    {

        private void GetPrinters ()
        {
            DropDownList1.Items.Clear();
            // Create a new DataTable.
            System.Data.DataTable DTBL_MAIN = new DataTable("MyTable");
            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.  

            DataColumn workCol = DTBL_MAIN.Columns.Add("Группа", typeof(String));
            workCol.AllowDBNull = false;
            workCol.Unique = false;

            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(strConnString);
            string cmd_prn2 = "SELECT * FROM ListOfGroup";
            DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
            if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count>0))  {
            for (int i = 0; i < DTBL_ListPRN.Rows.Count; i++)
            {
                DataRow row = DTBL_MAIN.NewRow();
                row["Группа"] = DTBL_ListPRN.Rows[i][1];
                DropDownList1.Items.Add(DTBL_ListPRN.Rows[i][1].ToString());
                DTBL_MAIN.Rows.Add(row);
            }
            GV_listPrn.DataSource = DTBL_MAIN;
            GV_listPrn.DataBind();
        }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPrinters();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text !="") {
                var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                string strConnString = conString.ConnectionString;
                SqlConnection sqlConn = new SqlConnection(strConnString);
                SqlCommand sqlComm = new SqlCommand();
                sqlComm = sqlConn.CreateCommand();
                sqlComm.CommandText = @"INSERT INTO ListOfGroup (Код, Имя_группы) VALUES (@Код, @Имя_группы)";
                sqlComm.Parameters.Add("@Код", SqlDbType.VarChar);
                sqlComm.Parameters["@Код"].Value = 1;
                sqlComm.Parameters.Add("@Имя_группы", SqlDbType.VarChar);
                sqlComm.Parameters["@Имя_группы"].Value = TextBox1.Text;
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                GetPrinters();  
            }
           
           
        }

        protected void GV_listPrn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Delete_group(string grp)
        {
            try
            {
                var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                string strConnString = conString.ConnectionString;
                SqlConnection sqlConn = new SqlConnection(strConnString);
                SqlCommand sqlComm = new SqlCommand();
                sqlComm = sqlConn.CreateCommand();
                sqlComm.CommandText = @"UPDATE spprinter SET Группа_принтера= 'unknown'  WHERE Группа_принтера= @grp";
                sqlComm.Parameters.Add("@grp", SqlDbType.VarChar);
                sqlComm.Parameters["@grp"].Value = grp;
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception Ex)
            {
                return;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex != -1)
            {
                Delete_group(DropDownList1.SelectedValue);
                var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                string strConnString = conString.ConnectionString;
                SqlConnection sqlConn = new SqlConnection(strConnString);
                SqlCommand sqlComm = new SqlCommand();
                sqlComm = sqlConn.CreateCommand();
                sqlComm.CommandText = @"DELETE FROM ListOfGroup WHERE [Имя_группы]='" + DropDownList1.SelectedValue + "'";
                //sqlComm.Parameters.Add("@Код", SqlDbType.VarChar);
                //sqlComm.Parameters["@Код"].Value = 1;
                //sqlComm.Parameters.Add("@Имя_группы", SqlDbType.VarChar);
                //sqlComm.Parameters["@Имя_группы"].Value = TextBox1.Text;
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                GetPrinters();  
            }
        }

    }
}