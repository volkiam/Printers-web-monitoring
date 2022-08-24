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
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                sqlComm.CommandText = @"INSERT INTO PrnProfile (Код, Имя_группы) VALUES (@Код, @Имя_группы)";
                sqlComm.Parameters.Add("@Код", SqlDbType.VarChar);
                sqlComm.Parameters["@Код"].Value = 1;
                sqlComm.Parameters.Add("@Имя_группы", SqlDbType.VarChar);
                sqlComm.Parameters["@Имя_группы"].Value = TextBox1.Text;
                sqlConn.Open();
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                
            }
        }
    }
}