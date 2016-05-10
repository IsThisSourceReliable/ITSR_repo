using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.USER;
using System.Data;

namespace ITSR
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadProfilePage(21);
        }

        public void LoadProfilePage(int iduser)
        {
            Member m = new Member();

            m.ID = iduser;
            m.LoadUser(m);
            m.LoadUserProfile(m);

            lbluserName.Text = m.userName;
            lblFullNAme.Text = m.firstname + " " + m.lastName;
            lblCountry.Text = m.country;
            lblLocation.Text = m.location;
            lblOccupation.Text = m.occupation;
            lblAboutme.Text = m.aboutme;
            

            populateGvMyArticles(m.GetArticlesCreatedBy(m));
            populateGvVotes(m.GetLastVotesBy(m));
        }
        public void populateGvMyArticles(DataTable dt)
        {
            gvMyArticles.DataSource = dt;
            gvMyArticles.DataBind();
        }
        public void populateGvVotes(DataTable dt)
        {
            gvMyVotes.DataSource = dt;
            gvMyVotes.DataBind();
        }

        protected void gvMyArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["articleID"] = Convert.ToInt32(gvMyArticles.SelectedValue);
            Response.Redirect("~/Article.aspx");
        }
        protected void gvMyArticles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Attaching one onclick event for the entire row, so that it will fire SelectedIndexChanged, while we click anywhere on the row.
                e.Row.Attributes["onclick"] =
                  ClientScript.GetPostBackClientHyperlink(gvMyArticles, "Select$" + e.Row.RowIndex);
            }
        }
        protected void gvMyVotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Attaching one onclick event for the entire row, so that it will fire SelectedIndexChanged, while we click anywhere on the row.
                e.Row.Attributes["onclick"] =
                  ClientScript.GetPostBackClientHyperlink(gvMyVotes, "Select$" + e.Row.RowIndex);
            }
        }
        protected void gvMyVotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["articleID"] = Convert.ToInt32(gvMyVotes.SelectedValue);
            Response.Redirect("~/Article.aspx");
        }
    }
}