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

            if (!IsPostBack)
            {
                if (Session["profileID"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    LoadProfilePage(Convert.ToInt32(Session["profileID"]));
                }             
            }

            if (Session["profileID"] == null)
            {
                Response.Redirect("~/default.aspx");
            }


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

            lvCreatedArticles.DataSource = m.GetArticlesCreatedBy(m);
            lvCreatedArticles.DataBind();

            lvLastVoted.DataSource = m.GetLastVotesBy(m);
            lvLastVoted.DataBind();

            lvLastCommented.DataSource = m.GetLastCommentsBy(m);
            lvLastCommented.DataBind();
        }

        protected void lvCreatedArticles_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvCreatedArticles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNamevalue = e.CommandName.ToString();
            string CommandArgument = e.CommandArgument.ToString();
            
            switch (CommandNamevalue)
            {
                case "GoToArticle":
                    Session["ArticleID"] = CommandArgument;
                    Response.Redirect("~/Article.aspx");
                    break;

            }
        }

        protected void lvLastCommented_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvLastCommented_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void lvLastVoted_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvLastVoted_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
    }
}