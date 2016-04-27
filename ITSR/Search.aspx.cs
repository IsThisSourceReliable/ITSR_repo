using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.ARTICLE;
using System.Drawing;

namespace ITSR
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Articles a = new Articles();
            //gvArticles.DataSource = a.LoadAllArticles();
            //gvArticles.DataBind();


        }

        protected void gvArticles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Attaching one onclick event for the entire row, so that it will fire SelectedIndexChanged, while we click anywhere on the row.
                e.Row.Attributes["onclick"] =
                  ClientScript.GetPostBackClientHyperlink(this.gvArticles, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("~/Article.aspx");
            
        }
    }
}