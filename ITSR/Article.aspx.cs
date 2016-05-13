using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using ITSR.CLASSES.ARTICLE;
using System.IO;

namespace ITSR
{
    public partial class Article : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {

            }

            if (!IsPostBack)
            {
                LoadArticle();
                LoadComments();
                //ListView1.DataSource = GetStuff();
                //ListView1.DataBind();
                lblCommenLogin.Visible = false;
            }
        }

        /// <summary>
        /// Method loads article and sets lables and relevant values. 
        /// </summary>
        private void LoadArticle()
        {
            string articleID = Session["ArticleID"].ToString();
            Articles getArticle = new Articles();
            getArticle.ID = Convert.ToInt32(articleID);
            DataTable dt = getArticle.GetArticle();

            string referenceXML = dt.Rows[0]["reference_xml"].ToString();

            getArticle.upVotes = int.Parse(dt.Rows[0]["votes_up"].ToString());
            getArticle.downVotes = int.Parse(dt.Rows[0]["votes_down"].ToString());

            int totalVotes = getArticle.SetTotalVotes();
            double upVotePercent = getArticle.SetUpVotePercent(totalVotes);
            double downVotePercent = getArticle.SetDownVotesPercent(upVotePercent);

            SetArticleLables(dt);
            BindReferences(referenceXML);
            SetVotes(totalVotes, upVotePercent, downVotePercent);
        }

        /// <summary>
        /// Method sets all the labels in the article uses datatable which is
        /// retrived from Loadarticle method.
        /// </summary>
        /// <param name="dt"></param>
        private void SetArticleLables(DataTable dt)
        {
            hiddenArticleID.Value = dt.Rows[0]["idarticle"].ToString();
            lblArticleName.Text = dt.Rows[0]["title"].ToString();
            lblTypeOfOrg.Text = dt.Rows[0]["orgtype"].ToString();
            lblUpHouseMan.Text = dt.Rows[0]["publisher"].ToString();
            lblDomainOwner.Text = dt.Rows[0]["domainowner"].ToString();
            lblFinancer.Text = dt.Rows[0]["financing"].ToString();
            lblEditDate.Text = dt.Rows[0]["lastedit_date"].ToString();
            linkBtnLastEdit.Text = dt.Rows[0]["edituser"].ToString();
            articleText.InnerHtml = dt.Rows[0]["text"].ToString();
        }

        /// <summary>
        /// This method binds the listview and referenses tougheter. 
        /// </summary>
        /// <param name="xml"></param>
        private void BindReferences(string xml)
        {
            if (xml == string.Empty)
            {
                lblRefText.Text = "This source article doesn't have any references";
            }
            else
            {
                lblRefText.Text = "";
                DataTable dt = ReadXMLReferences(xml);
                ListViewReferences.DataSource = dt;
                ListViewReferences.DataBind();
            }
        }

        /// <summary>
        /// This method reads the xml reference file and returns it as a datatable
        /// to be able to bind it with a listview.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private DataTable ReadXMLReferences(string xml)
        {
            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[0];
        }

        /// <summary>
        /// Method sets the upvotes for an article and sets the width of
        /// the vote bar.
        /// </summary>
        /// <param name="totalVotes"></param>
        /// <param name="upVotes"></param>
        /// <param name="downVotes"></param>
        private void SetVotes(int totalVotes, double upVotes, double downVotes)
        {
            lblTotalVotes.Text = totalVotes.ToString();
            upvoteBar.Style.Add("width", "" + upVotes + "%");
            downvoteBar.Style.Add("width", "" + downVotes + "%");
        }


        /// <summary>
        /// Method loads all the comments to an article.
        /// </summary>
        private void LoadComments()
        {
            Comment GetArticleComments = new Comment();
            DataTable dt = new DataTable();
            GetArticleComments.Article_id = int.Parse(Session["ArticleID"].ToString());
            dt = GetArticleComments.GetComments();
            if (dt.Rows.Count == 0)
            {
                lblNoComments.Visible = true;
                lblNoComments.Text = "There is no comments on this article, be the first one to comment!";            
            }
            else
            {
                lblNoComments.Visible = false;

                listViewComments.DataSource = dt;
                listViewComments.DataBind();
            }
        }

        /// <summary>
        /// Just for testpurpose
        /// </summary>
        /// <returns></returns>
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
            string listViewIndex = e.Item.DataItemIndex.ToString();
            //string dataBaseIndex = e.CommandArgument.ToString();
            Label lbltext = (Label)e.Item.FindControl("Label2");
            HiddenField daHiddenField = (HiddenField)e.Item.FindControl("HiddenCommentID");
            string dataBaseIndex = daHiddenField.Value.ToString();

            switch (value)
            {
                case "ReportComment":
                    ReportComment(listViewIndex, dataBaseIndex, lbltext);
                    break;

                case "DeleteComment":
                    DeleteComment(listViewIndex, dataBaseIndex, lbltext);
                    break;

                default:
                    //Label3.Text = "Default";
                    break;
            }

        }

        private void ReportComment(string listViewIndex, string dataBaseIndex, Label lbltext)
        {
            //Label3.Text = "Report!";
            lblIndexListView.Text = listViewIndex;
            lblIndexDataBase.Text = dataBaseIndex;
            lblUserName.Text = lbltext.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
        }

        private void DeleteComment(string listViewIndex, string dataBaseIndex, Label lbltext)
        {
            //Label3.Text = "Delete!";
            lblIndexListView.Text = listViewIndex;
            lblIndexDataBase.Text = dataBaseIndex;
            lblUserName.Text = lbltext.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
        }

        protected void lBtnEdit_Click(object sender, EventArgs e)
        {           
            Session["ArticleID"] = hiddenArticleID.Value.ToString();
            Response.Redirect("~/EditArticle.aspx");
        }

        protected void btnPostComment_Click(object sender, EventArgs e)
        {
            if(Session["UserID"] == null)
            {
                lblCommenLogin.Text = "You have to login to post a comment.";
                lblCommenLogin.Visible = true;
            }
            else
            {
                Comment newComment = new Comment();

                newComment.CommentText = txtComment.Text;
                newComment.User_id = int.Parse(Session["UserID"].ToString());
                newComment.Article_id = int.Parse(hiddenArticleID.Value.ToString());
                newComment.Removed = false;
                newComment.Date = DateTime.Now;

                newComment.InsertComment();
                txtComment.Text = string.Empty;
                lblCommenLogin.Visible = false;

                LoadComments();
            }

        }

        protected void listViewComments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string value = e.CommandName.ToString();
            string listViewIndex = e.Item.DataItemIndex.ToString();
            //string dataBaseIndex = e.CommandArgument.ToString();
            //Label lbltext = (Label)e.Item.FindControl("Label2");
            HiddenField commentID = (HiddenField)e.Item.FindControl("HiddenCommentID");
            HiddenField commentUserID = (HiddenField)e.Item.FindControl("HiddenUserID");
            Label userNameLbl = (Label)e.Item.FindControl("lblCommentUserName");
            Label  txtCommentLbl = (Label)e.Item.FindControl("lblCommentText");

            //switch (value)
            //{
            //    case "ReportComment":
            //        ReportComment(listViewIndex, dataBaseIndex, lbltext);
            //        break;

            //    case "DeleteComment":
            //        DeleteComment(listViewIndex, dataBaseIndex, lbltext);
            //        break;

            //    default:
            //        //Label3.Text = "Default";
            //        break;
            //}
        }
    }
}