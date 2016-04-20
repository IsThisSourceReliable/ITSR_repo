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
    }
}