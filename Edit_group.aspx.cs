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
    public partial class WebForm4 : System.Web.UI.Page
    {

         private void GetGroup_to_List()
        {
            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(@strConnString);
            DDL2.Items.Clear();
            DDL1.Items.Clear();
            DDL2.Items.Add("Все");
            string cmd_prn2 = "SELECT * FROM ListOfGroup";
            DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
            if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count > 0))
            {
                for (int i = 0; i < DTBL_ListPRN.Rows.Count; i++)
                {
                    DDL2.Items.Add(DTBL_ListPRN.Rows[i][1].ToString());
                    DDL1.Items.Add(DTBL_ListPRN.Rows[i][1].ToString());
                }
                DDL2.Items.Add("unknown");
                DDL1.Items.Add("unknown");
                
            }
         }


        private void GetPrinters_to_List(string mygroup)
        {
            CBL1.Items.Clear();
            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SQL_MANAGER SM_ListPRN = new SQL_MANAGER(@strConnString);

            string cmd_prn2 = "";
            if (mygroup == "Все")
            {
                cmd_prn2 = "SELECT * FROM spprinter";                       
            }
            else
            {
                cmd_prn2 = "SELECT * FROM spprinter Where [Группа_принтера]='"+mygroup+"'";
            }

            DataTable DTBL_ListPRN = SM_ListPRN.SQL_SELECTOR(cmd_prn2, false, false);
            if ((DTBL_ListPRN != null) & (DTBL_ListPRN.Rows.Count > 0))
            {
                for (int i = 0; i < DTBL_ListPRN.Rows.Count; i++)
                    {
                        CBL1.Items.Add(DTBL_ListPRN.Rows[i][1].ToString() + "(" + DTBL_ListPRN.Rows[i][3].ToString()+")");
                    }

             }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DDL2.SelectedItem == null) GetGroup_to_List();
                GetPrinters_to_List("Все");
                CBL1.Width = DDL2.Width;
            }  
        }

        protected void DDL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPrinters_to_List(DDL2.SelectedValue);
            CBL1.Width = DDL2.Width;
           
        }

        protected void Add_group(string itm,string grp)
        {
            try
            {
            int iStart = itm.IndexOf("(");
            int iEnd = itm.Length-iStart;
            string itm1 = itm.Substring(iStart+ 1,  iEnd- 2);
            string itm2 = itm.Substring(0,iStart);
            var conString = ConfigurationManager.ConnectionStrings["prnBaseConnectionString"];
            string strConnString = conString.ConnectionString;
            SqlConnection sqlConn = new SqlConnection(strConnString);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm = sqlConn.CreateCommand();
            sqlComm.CommandText = @"UPDATE spprinter SET Группа_принтера= @grp  WHERE [IP]=@IP";
            sqlComm.Parameters.Add("@grp", SqlDbType.VarChar);
            sqlComm.Parameters["@grp"].Value = grp;
            sqlComm.Parameters.Add("@IP", SqlDbType.VarChar);
            sqlComm.Parameters["@IP"].Value = itm1;
            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
                            }
            catch (Exception Ex)
            {
                 return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CBL1.Items.Count; i++)
            {
                if (CBL1.Items[i].Selected)
                {
                    Add_group(CBL1.Items[i].Value.Replace(" ", string.Empty), DDL1.Text.TrimEnd());
                    
                }
            }
            GetPrinters_to_List(DDL2.SelectedValue);
        }


      
    }
}