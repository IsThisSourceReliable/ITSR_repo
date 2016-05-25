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

            DataTable dtCreated = m.GetArticlesCreatedBy(m);
            DataTable dtVoted = m.GetLastVotesBy(m);
            DataTable dtComment = m.GetLastCommentsBy(m);

            CountRows(dtCreated, dtVoted, dtComment);

            lvCreatedArticles.DataSource = dtCreated;
            lvCreatedArticles.DataBind();

            lvLastVoted.DataSource = dtVoted;
            lvLastVoted.DataBind();

            lvLastCommented.DataSource = dtComment;
            lvLastCommented.DataBind();

            
        }

        public void CountRows (DataTable dtCreated, DataTable dtVoted, DataTable dtCommented)
        {
            int created, comments, votes = 0;

            created = dtCreated.Rows.Count;
            comments = dtCommented.Rows.Count;
            votes = dtVoted.Rows.Count;

            if (created == 0)
            {
                NoCreatedArticles.IsValid = false;
            }
            if (comments == 0)
            {
                NoComments.IsValid = false;
            }
            if (votes == 0)
            {
                NoVotes.IsValid = false;
            }

            
        }

        protected void lvCreatedArticles_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Member m = new Member();
            m.ID = Convert.ToInt32(Session["profileID"]);
            DataTable dtCreated = m.GetArticlesCreatedBy(m);
            DataTable dtVoted = m.GetLastVotesBy(m);
            DataTable dtComment = m.GetLastCommentsBy(m);

            CountRows(dtCreated, dtVoted, dtComment);

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

        protected void lvLastVoted_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvLastVoted_ItemCommand(object sender, ListViewCommandEventArgs e)
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
    }
}