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
        protected void Page_Init(object sender, EventArgs e)
        {
            var master = (MainMaster)Page.Master;
            master.UpdateArticlePage += MasterSelected;
            //master.OnSomethingSelected += MasterSelected;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                HideFromNotLoggedIn();
            }

            if (!IsPostBack)
            {
                LoadArticle();
                lblCommenLogin.Visible = false;
                lblVoteLogin.Visible = false;
            }
        }

        /// <summary>
        /// Method currently only updats the comments section.
        /// Can be used to update more things on the conent page
        /// on login from master page.
        /// </summary>
        /// 
        private void MasterSelected()
        {
            LoadComments(hiddenArticleID.Value.ToString());
            SetVoteButton();
            ShowToLoggedIn();
        }

        /// <summary>
        /// Method loads article and sets lables and relevant values. 
        /// </summary>
        /// 
        private void LoadArticle()
        {
            string articleID = Session["ArticleID"].ToString();
            Articles getArticle = new Articles();
            getArticle.ID = Convert.ToInt32(articleID);
            DataTable dt = getArticle.GetArticle();

            getArticle.upVotes = int.Parse(dt.Rows[0]["votes_up"].ToString());
            getArticle.downVotes = int.Parse(dt.Rows[0]["votes_down"].ToString());

            int totalVotes = getArticle.SetTotalVotes();
            double upVotePercent = getArticle.SetUpVotePercent(totalVotes);
            double downVotePercent = getArticle.SetDownVotesPercent(upVotePercent);

            SetArticleLables(dt);
            SetVotes(totalVotes, upVotePercent, downVotePercent);
            LoadComments(articleID);
            SetVoteButton();
        }

        /// <summary>
        /// Method sets all the labels in the article uses datatable which is
        /// retrived from Loadarticle method.
        /// </summary>
        /// <param name="dt"></param>
        private void SetArticleLables(DataTable dt)
        {
            hiddenArticleID.Value = dt.Rows[0]["idarticle"].ToString();
            HiddenLastEditByID.Value = dt.Rows[0]["lastedituser_id"].ToString();
            lblArticleName.Text = dt.Rows[0]["title"].ToString();
            lblTypeOfOrg.Text = dt.Rows[0]["orgtype"].ToString();
            lblUpHouseMan.Text = dt.Rows[0]["publisher"].ToString();
            lblDomainOwner.Text = dt.Rows[0]["domainowner"].ToString();
            lblFinancer.Text = dt.Rows[0]["financing"].ToString();
            lblEditDate.Text = dt.Rows[0]["lastedit_date"].ToString();
            linkBtnLastEdit.Text = dt.Rows[0]["edituser"].ToString();
            articleText.InnerHtml = dt.Rows[0]["text"].ToString();

            lblArticle.Text = lblArticleName.Text;
            CreatorIDOverlay.Value = dt.Rows[0]["createuser_id"].ToString();

            string referenceXML = dt.Rows[0]["reference_xml"].ToString();
            BindReferences(referenceXML);
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
        /// Method evaluates if user has voted on an article or not. 
        /// If user has voted it calls SetVoteButtonProperties
        /// method to set relevant properties for button.
        /// </summary>
        private void SetVoteButton()
        {
            if(Session["UserID"] != null)
            {
                DataTable dt = new DataTable();
                Vote CheckVote = new Vote();
                bool voteType = false;
                CheckVote.User_id = int.Parse(Session["UserID"].ToString());
                CheckVote.Article_id = int.Parse(hiddenArticleID.Value);

                dt = CheckVote.GetUserVote();
                if (dt.Rows.Count > 0)
                {
                    int intVote = Convert.ToInt32(dt.Rows[0]["vote"].ToString());
                    if (intVote == 0)
                    {
                        SetVoteButtonProperties(voteType);
                    }
                    else
                    {
                        voteType = true;
                        SetVoteButtonProperties(voteType);
                    }
                }
            }
        }

        /// <summary>
        /// Method sets properties for upvote and downvote button depending if user has
        /// upvoted or downvoted.
        /// </summary>
        /// <param name="voteType"></param>
        private void SetVoteButtonProperties(bool voteType)
        {
            if(voteType)
            {
                lBtnUpvote.Enabled = false;
                lBtnUpvote.CssClass = "vote-btn-pressed";
                upvoteGlyph.Attributes["class"] = "upvote-glyph glyphicon glyphicon-arrow-up ";
                lBtnUpvote.ToolTip = "You have already upvoted this source";

                lBtnDownVote.ToolTip = "Click here to downvote";
                lBtnDownVote.CssClass = "vote-btn";
                downvoteGlyph.Attributes["class"] = "vote-glyph glyphicon glyphicon-arrow-down ";
                lBtnDownVote.Enabled = true;
            }
            else
            {
                lBtnDownVote.Enabled = false;
                lBtnDownVote.CssClass = "vote-btn-pressed";
                downvoteGlyph.Attributes["class"] = "downvote-glyph glyphicon glyphicon-arrow-down ";
                lBtnDownVote.ToolTip = "You have already downvoted this source";

                lBtnUpvote.ToolTip = "Click here to upvote";
                lBtnUpvote.CssClass = "vote-btn";
                upvoteGlyph.Attributes["class"] = "vote-glyph glyphicon glyphicon-arrow-up ";
                lBtnUpvote.Enabled = true;
            }
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
        private void LoadComments(string articleID)
        {
            string order = dropDownSortComments.SelectedValue;
            int limit = int.Parse(DropDownLimitComment.SelectedValue);

            Comment GetArticleComments = new Comment();
            DataTable dt = new DataTable();
            DataTable dtCount = new DataTable(); //Try to figure out how to include a count in GetComments insted. 

            GetArticleComments.Article_id = int.Parse(articleID);
            GetArticleComments.Order = order;
            GetArticleComments.Limit = limit;

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

                dtCount = GetArticleComments.CountComments();
                lblTotalComments.Text = dtCount.Rows[0]["totalcomments"].ToString();
            }
        }

        /// <summary>
        /// Method updates the info inside the votebox when a user 
        /// votes on a source. 
        /// </summary>
        private void UpdateVoteBox()
        {
            Articles UpdateVote = new Articles();
            DataTable dt = new DataTable();

            UpdateVote.ID = int.Parse(hiddenArticleID.Value.ToString());

            dt = UpdateVote.GetArticleVotes();

            UpdateVote.upVotes = int.Parse(dt.Rows[0]["votes_up"].ToString());
            UpdateVote.downVotes = int.Parse(dt.Rows[0]["votes_down"].ToString());

            int totalVotes = UpdateVote.SetTotalVotes();
            double upVotePercent = UpdateVote.SetUpVotePercent(totalVotes);
            double downVotePercent = UpdateVote.SetDownVotesPercent(upVotePercent);

            SetVotes(totalVotes, upVotePercent, downVotePercent);
        }

        /// <summary>
        /// This method sets the labels in the overlay to correct values and
        /// also the hiddenfields to correct values so a user can report a comment.
        /// </summary>
        /// <param name="commentID"></param>
        /// <param name="userID"></param>
        /// <param name="commentText"></param>
        /// <param name="username"></param>
        private void SetOverlayLabels(string commentID, string userID, string commentText, string username)
        {
            CommentIDOverlay.Value = commentID;
            CommenUserIDOverlay.Value = userID;
            lblCommentTextOverlay.Text = commentText;
            lblUserNameComment.Text = username;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenOverlay", "OpenOverlay();", true);
        }

        /// <summary>
        /// This method sets the labels in the overlay to correct values and
        /// also the hiddenfields to correct values so a user can report a comment.
        /// </summary>
        /// <param name="commentID"></param>
        /// <param name="userID"></param>
        /// <param name="commentText"></param>
        /// <param name="username"></param>

        /// <summary>
        /// TO BE IMPLEMENTED ***********************************************************************************
        /// *****************************************************************************************************
        /// </summary>
        /// <param name="listViewIndex"></param>
        /// <param name="dataBaseIndex"></param>
        /// <param name="lbltext"></param>
        private void DeleteComment(string listViewIndex, string dataBaseIndex, Label lbltext)
        {
            //Label3.Text = "Delete!";
            //lblIndexListView.Text = listViewIndex;
            //lblIndexDataBase.Text = dataBaseIndex;
            //lblUserName.Text = lbltext.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenOverlay", "OpenOverlay()", true);
        }

        /// <summary>
        /// Event for when edit button is clicked. 
        /// Redirecs user to edit page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnEdit_Click(object sender, EventArgs e)
        {           
            Session["ArticleID"] = hiddenArticleID.Value.ToString();
            Response.Redirect("~/EditArticle.aspx");
        }

        /// <summary>
        /// Event for when user click post comment. Sets values in 
        /// comment class and inserts comment and updates the listview 
        /// with latest comment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string articleID = hiddenArticleID.Value.ToString();
                newComment.CommentText = txtComment.Text;
                newComment.User_id = int.Parse(Session["UserID"].ToString());
                newComment.Article_id = int.Parse(articleID);
                newComment.Removed = false;
                newComment.Date = DateTime.Now;

                newComment.InsertComment();
                txtComment.Text = string.Empty;
                lblCommenLogin.Visible = false;

                LoadComments(articleID);
            }

        }

        /// <summary>
        /// Event for listview event command. Currently takes two commands
        /// Reportcomment and Deletecomment. TO DO implement so when a user clicks username
        /// user is redirected to that users profile page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void listViewComments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string value = e.CommandName.ToString();
            string listViewIndex = e.Item.DataItemIndex.ToString();
            //string dataBaseIndex = e.CommandArgument.ToString();
            //Label lbltext = (Label)e.Item.FindControl("Label2");
            HiddenField commentID = (HiddenField)e.Item.FindControl("HiddenCommentID");
            HiddenField userID = (HiddenField)e.Item.FindControl("HiddenUserID");
            HiddenField hiddenRemoved = (HiddenField)e.Item.FindControl("HiddenRemoved");
            Label userNameLbl = (Label)e.Item.FindControl("lblCommentUserName");
            Label txtCommentLbl = (Label)e.Item.FindControl("lblCommentText");

            int removed = int.Parse(hiddenRemoved.Value.ToString());
            string sCommentID = commentID.Value.ToString();
            string sUserID = userID.Value.ToString();
            string sUserName = userNameLbl.Text;
            string sTxtComment = txtCommentLbl.Text;
            
            if(removed != 1)
            {
                switch (value)
                {
                    case "ReportComment":
                        SetOverlayLabels(sCommentID, sUserID, sTxtComment, sUserName);
                        //ReportComment(listViewIndex, dataBaseIndex, lbltext);
                        break;

                    case "DeleteComment":
                        //DeleteComment(listViewIndex, dataBaseIndex, lbltext);
                        break;
                    case "VisitProfile":
                        VisitProfile(sUserID);
                        break;
                    default:
                        //Label3.Text = "Default";
                        break;
                }
            }
        }
        public void VisitProfile(string profileid)
        {
            Session["profileID"] = profileid;
            Response.Redirect("~/Profile.aspx");
        }

        /// <summary>
        /// Event for when items are databound in the listview. Hides and shows 
        /// linkbuttons depending on users memberlevel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void listViewComments_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var linkButtonReport = (LinkButton)e.Item.FindControl("lBtnReport");
                var linkButtonDelete = (LinkButton)e.Item.FindControl("lBtnDelete");
                HiddenField hiddenRemoved = (HiddenField)e.Item.FindControl("HiddenRemoved");
                Label txtCommentLbl = (Label)e.Item.FindControl("lblCommentText");

                int removed = int.Parse(hiddenRemoved.Value.ToString());

                if (Session["UserID"] == null) 
                {
                    linkButtonReport.Visible = false;
                    linkButtonDelete.Visible = false;
                }
                else
                {
                    linkButtonReport.Visible = true;
                    linkButtonDelete.Visible = false;
                }
                //else if(UserIsAMember)
                //{
                //    linkButtonReport.Visible = true;
                //    linkButtonDelete.Visible = false;
                //}
                //else //Is a user is an admin show everything.
                //{
                //    linkButtonReport.Visible = true;
                //    linkButtonDelete.Visible = true;
                //}

                if (removed == 1)
                {
                    txtCommentLbl.Text = "This comment has been removed by a moderator.";
                    txtCommentLbl.CssClass = "fail-text";
                    linkButtonReport.Visible = false;
                    linkButtonDelete.Visible = false;
                }
            }
        }

        /// <summary>
        /// Click event for when user clicks report comments. Sets relevant values
        /// and calls js script to close overlay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Comment Report = new Comment();

            Report.ID = int.Parse(CommentIDOverlay.Value.ToString());
            Report.User_id = int.Parse(Session["UserID"].ToString());
            Report.ReportReason = txtReason.Text;

            Report.ReportComment();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CloseOverlay", "CloseOverlay();", true);
        }

        /// <summary>
        /// Event for when user changes value in dropdown to sort comments from first and latest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropDownSortComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dropDownSortComments.SelectedIndex > -1)
            {
                string articleID = hiddenArticleID.Value.ToString();
                LoadComments(articleID);
            }
        }

        /// <summary>
        /// Event for when user chooses how many comments to be seen on screen. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownLimitComment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownLimitComment.SelectedIndex > -1)
            {
                string articleID = hiddenArticleID.Value.ToString();
                LoadComments(articleID);
            }
        }

        /// <summary>
        /// Event for when user clicks tp upvote. 
        /// Uses method in vote class in order to set correct vote status.
        /// Calls methoed SetVoteButtonProperties to update votebutton
        /// and UpdateVoteBox to update info in votebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnUpvote_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblVoteLogin.Text = "You have to log in to vote";
                lblVoteLogin.Visible = true;
            }
            else
            {
                bool voteType = true;

                Vote UpVote = new Vote();
                UpVote.User_id = int.Parse(Session["UserID"].ToString());
                UpVote.Article_id = int.Parse(hiddenArticleID.Value);
                UpVote.VoteType = voteType;

                if (UpVote.SetVote())
                {
                    //lblVoteLogin.Text = "You upvoted! YEY"; // Test purpose
                    //lblVoteLogin.Visible = true;              //TEst
                    UpdateVoteBox();
                    SetVoteButtonProperties(voteType);
                }
                else
                {
                    //lblVoteLogin.Text = "Already upveted upvoted! YEY"; //test
                    //lblVoteLogin.Visible = true; //Test
                }

                //lblVoteLogin.Visible = false;
            }
        }

        /// <summary>
        /// Event for when user clicks tp downvotw. 
        /// Uses method in vote class in order to set correct vote status.
        /// Calls methoed SetVoteButtonProperties to update votebutton
        /// and UpdateVoteBox to update info in votebox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnDownVote_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblVoteLogin.Text = "You have to log in to vote";
                lblVoteLogin.Visible = true;
            }
            else
            {
                bool voteType = false;

                Vote DownVote = new Vote();
                DownVote.User_id = int.Parse(Session["UserID"].ToString());
                DownVote.Article_id = int.Parse(hiddenArticleID.Value);
                DownVote.VoteType = voteType;

                if (DownVote.SetVote())
                {
                    //lblVoteLogin.Text = "You Downvoted! YEY"; //Test
                    //lblVoteLogin.Visible = true; //Test
                    UpdateVoteBox();
                    SetVoteButtonProperties(voteType);
                }
                else
                {
                    //lblVoteLogin.Text = "Already downvoted downvoted! YEY"; //Test
                    //lblVoteLogin.Visible = true; //Test
                }
                //lblVoteLogin.Visible = false;
            }
        }

        protected void BtnReportArticle2_Click(object sender, EventArgs e)
        {
            Articles a = new Articles();
            Report r = new Report();

            r.articleORcomment_id = Convert.ToInt32(hiddenArticleID.Value);
            r.text = tbReportReason.Text;
            r.user_id = Convert.ToInt32(Session["userID"]);

            a.ReportArticle(r);

            tbReportReason.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CloseOverlay2", "CloseOverlay2();", true);
        }

        protected void HideFromNotLoggedIn()
        {
            reportText.Visible = false;
            editText.Visible = false;
        }

        protected void ShowToLoggedIn()
        {
            reportText.Visible = true;
            editText.Visible = true;
        }

        protected void btnLinkTest_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/DaTest.aspx");
            //Session["id"] = 
        }

        protected void linkBtnLastEdit_Click(object sender, EventArgs e)
        {
            Session["profileID"] = HiddenLastEditByID.Value;
            Response.Redirect("~/Profile.aspx");
        }
    }
}