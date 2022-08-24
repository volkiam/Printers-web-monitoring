using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        private void GetSensors()
        {
            
            // Create a new DataTable.
            //System.Data.DataTable DTBL_MAIN = new DataTable("MyTable");
            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.  

            //DataColumn workCol = DTBL_MAIN.Columns.Add("Группа", typeof(String));
            //workCol.AllowDBNull = false;
            //workCol.Unique = false;

            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(strConnString);
            string cmd_prn2 = "SELECT * FROM PrnSensors";
            DataTable DTBL_SensorsPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
            if ((DTBL_SensorsPRN != null) & (DTBL_SensorsPRN.Rows.Count > 0))
            {
                GV_SensorsPrn.DataSource = DTBL_SensorsPRN;
                GV_SensorsPrn.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSensors();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
                string strConnString = conString.ConnectionString;
                SqlConnection sqlConn = new SqlConnection(strConnString);
                SqlCommand sqlComm = new SqlCommand();
                sqlComm = sqlConn.CreateCommand();
                sqlComm.CommandText = @"INSERT INTO PrnSensors (SensorName, SensorType) VALUES (@SensorName, @SensorType)";
                sqlComm.Parameters.Add("@SensorName", SqlDbType.VarChar);
                sqlComm.Parameters["@SensorName"].Value = TextBox1.Text;
                sqlComm.Parameters.Add("@SensorType", SqlDbType.VarChar);
                sqlComm.Parameters["@SensorType"].Value = DropDownList1.Text;
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                GetSensors();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}