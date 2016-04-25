using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITSR
{
    public partial class Article : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListView1.DataSource = GetStuff();
            ListView1.DataBind();
        }

        private DataTable GetStuff()
        {
            MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT iderik_table, erik_text FROM erik_table; ", conn);

                MySqlDataAdapter da = new MySqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                da.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }



        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLinkTest_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/DaTest.aspx");
            //Session["id"] = 
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //Label3.Text = "Index: " + e.Item.DataItemIndex.ToString() + " , Arg: " + e.CommandArgument;
            //if(e.CommandName == "ReportComment")
            //{
            //    Label3.Text = "Report!";
            //}
            string value = e.CommandName.ToString();
            switch (value)
            {
                case "ReportComment":
                    Label3.Text = "Report!";
                    lblIndexListView.Text = e.Item.DataItemIndex.ToString();
                    lblIndexDataBase.Text = e.CommandArgument.ToString();
                    Label lbltext = (Label)e.Item.FindControl("Label2");
                    lblUserName.Text = lbltext.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
                    break;

                case "DeleteComment":
                    Label3.Text = "Delete!";
                    lblIndexListView.Text = e.Item.DataItemIndex.ToString();
                    lblIndexDataBase.Text = e.CommandArgument.ToString();
                    Label lbltext1 = (Label)e.Item.FindControl("Label2");
                    lblUserName.Text = lbltext1.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
                    break;

                default:
                    Label3.Text = "Default";
                    break;
            }

        }
    }
}