using ITSR.CLASSES.ARTICLE;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ITSR
{
    public partial class testdef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) 
            {
                SetGUI();
            }
        }

        protected void linkBtnSearch_Click(object sender, EventArgs e)
        {
            Articles a = new Articles();
            DataTable dt;

            SetGUI();
            emptyGv();

            string searchString = txtSearchField.Text.ToString();

            dt = a.SearchForSpecificArticle(searchString);

            if (dt.Rows.Count == 0)
            {
                dt = a.SearchForUnspecificArticle(searchString);

                showMessage();

                if (dt.Rows.Count > 0)
                {
                    populateGv(dt);
                    showMessageAndGv();
                }

            }
            else if (dt.Rows.Count == 1)
            {
                dt = a.SearchForSpecificArticle(searchString);

                Response.Redirect("~/Article.aspx");
            }
           else if (dt.Rows.Count > 1)
            {
                dt = a.SearchForUnspecificArticle(searchString);
                populateGv(dt);
                showMessageAndGv();
            }
        }

        protected void lbNewARticle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Article.aspx");
        }


        //autocomplete searchstring
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetArticles(string prefixText)
        {

            MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

            string SS = "%" + prefixText + "%";
            string sql = "SELECT * FROM article WHERE title LIKE @SS OR url LIKE @SS";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@SS", SS);
            MySqlDataAdapter da = new MySqlDataAdapter();

            da.SelectCommand = cmd;

            DataTable dt = new DataTable();

            da.Fill(dt);
            conn.Close();
            List<string> articles = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                articles.Add(dt.Rows[i][1].ToString());
            }

            return articles;

        }


        //Methods
        public void SetGUI()
        {
            searchMessage.Visible = false;
            searchMessage2.Visible = false;
            messageDiv.Visible = false;
            //gvDiv.Visible = false;
        }
        public void emptyGv()
        {
            gvArticles.DataSource = "";
            gvArticles.DataBind();
        }
        public void populateGv(DataTable dt)
        {
            gvArticles.DataSource = dt;
            gvArticles.DataBind();
        }
        public void showMessage()
        {
            searchMessage.Visible = true;
            messageDiv.Visible = true;
        }
        public void showMessageAndGv()
        {
            searchMessage.Visible = false;
            searchMessage2.Visible = true;
            messageDiv.Visible = true;
            gvDiv.Visible = true;
        }
    }
}