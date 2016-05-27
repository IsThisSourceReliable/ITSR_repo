using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.ARTICLE;
using System.Data;
using System.IO;

namespace ITSR
{
    public partial class ModPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserID"] != null && int.Parse(Session["RoleID"].ToString()) >= 2)
            {
                if (!IsPostBack)
                {
                    LoadAndSetTotalReports();

                    ArticleBox.Visible = false;
                    dropDownDiv.Visible = false;
                    lBtnConfirmRevert.Visible = false;
                    lBtnCancelRevert.Visible = false;
                    lblFail.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }

        /// <summary>
        /// Method load all reported comments and binds with listview
        /// </summary>
        private void LoadReportComments()
        {
            Report Comments = new Report();
            DataTable dt = Comments.GetReportedComments();
            listViewReports.Visible = true;
            listViewReports.DataSource = dt;
            listViewReports.DataBind();
        }

        /// <summary>
        /// Method load all reported articles and binds with listview
        /// </summary>
        private void LoadReportedArticles()
        {

            Report ReportedArticles = new Report();
            DataTable dt = ReportedArticles.GetReportedArticles();
            ListViewReportArticles.Visible = true;
            ListViewReportArticles.DataSource = dt;
            ListViewReportArticles.DataBind();
        }

        /// <summary>
        /// Method hides the comment listview.
        /// </summary>
        private void HideCommentsReports()
        {
            listViewReports.DataSource = null;
            listViewReports.DataBind();
            listViewReports.Visible = false;
        }

        /// <summary>
        /// Method hides the article listview.
        /// </summary>
        private void HideArticlesReports()
        {
            ListViewReportArticles.DataSource = null;
            ListViewReportArticles.DataBind();
            ListViewReportArticles.Visible = false;
        }

        /// <summary>
        /// This method loads a reported article so that a moderator can see if there is
        /// anything wrong with said article.
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="reportArticleID"></param>
        private void LoadArticle(string articleID, string reportArticleID)
        {
            HideArticlesReports();
            ArticleBox.Visible = true;
            Articles getArticle = new Articles();
            getArticle.ID = Convert.ToInt32(articleID);
            DataTable dt = getArticle.GetArticle();

            HiddenArticleIDBox.Value = dt.Rows[0]["idarticle"].ToString();
            HiddenReportArticleID.Value = reportArticleID;
            SetArticleLabels(dt);
        }

        /// <summary>
        /// This method loads a copy of article from the copy table so a 
        /// moderator can preview before reverting back to an older version.
        /// </summary>
        /// <param name="articleCopyID"></param>
        private void LoadArticleCopy(int articleCopyID)
        {
            Articles copyArticle = new Articles();
            DataTable dt = copyArticle.GetArticleCopy(articleCopyID);
            SetArticleLabels(dt);
        }

        /// <summary>
        /// Method sets all the relevant article labels for an article.
        /// </summary>
        /// <param name="dt"></param>
        private void SetArticleLabels(DataTable dt)
        {
            lblArticleName.Text = dt.Rows[0]["title"].ToString();
            lblTypeOfOrg.Text = dt.Rows[0]["orgtype"].ToString();
            lblUpHouseMan.Text = dt.Rows[0]["publisher"].ToString();
            lblDomainOwner.Text = dt.Rows[0]["domainowner"].ToString();
            lblFinancer.Text = dt.Rows[0]["financing"].ToString();
            //lblEditDate.Text = dt.Rows[0]["lastedit_date"].ToString();
            //linkBtnLastEdit.Text = dt.Rows[0]["edituser"].ToString();
            articleText.InnerHtml = dt.Rows[0]["text"].ToString();

            string referenceXML = dt.Rows[0]["reference_xml"].ToString();
            BindReferences(referenceXML);

        }

        /// <summary>
        /// This method binds the listview and referenses tougheter. 
        /// </summary>
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
        private DataTable ReadXMLReferences(string xml)
        {
            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[0];
        }

        /// <summary>
        /// Method retrives and sets total reports label.
        /// </summary>
        private void LoadAndSetTotalReports()
        {
            Report TotalReports = new Report();
            int totalCommentsReport = TotalReports.GetTotalCommentsReports();
            int totalArticleReport = TotalReports.GetTotalArticleReports();
            lblTotalCommentReport.Text = totalCommentsReport.ToString() + " comments reports. ";
            lblTotalArticleReport.Text = totalArticleReport.ToString() + " article reports. ";
        }

        /// <summary>
        /// Method binds dropdown with old versions of an article with
        /// the dates so a moderator can choose what date to revert to.
        /// </summary>
        private void BindDropDownOldVersions()
        {
            Articles OldVersion = new Articles();
            OldVersion.ID = int.Parse(HiddenArticleIDBox.Value.ToString());
            DataTable dt = OldVersion.GetArticleCopyDates();

            dropDownOldVersion.DataSource = dt;
            dropDownOldVersion.DataTextField = "lastedit_date";
            dropDownOldVersion.DataValueField = "idarticlecopy";
            dropDownOldVersion.DataBind();
            this.dropDownOldVersion.Items.Insert(0, "Choose");
        }

        /// <summary>
        /// This method delets a comment that has been repoerted. Uses 
        /// report class to perfom said duty.
        /// </summary>
        /// <param name="reportcommentID"></param>
        /// <param name="commentID"></param>
        private void DeleteComment(int reportcommentID, int commentID)
        {
            Report Delete = new Report();
            Delete.ID = reportcommentID;
            Delete.articleORcomment_id = commentID;
            Delete.ModeratorUserID = int.Parse(Session["UserID"].ToString());
            Delete.RemoveReportedComment();
            LoadReportComments();
            LoadAndSetTotalReports();
        }

        /// <summary>
        /// This method only resolves a report on a comment, when a moderator decides that
        /// no action is requiered.
        /// </summary>
        /// <param name="reportcommentID"></param>
        /// <param name="commentID"></param>
        private void ResolveComment(int reportcommentID, int commentID)
        {
            Report Resolve = new Report();
            Resolve.ID = reportcommentID;
            Resolve.articleORcomment_id = commentID;
            Resolve.ModeratorUserID = int.Parse(Session["UserID"].ToString());
            Resolve.ResolveReportedComment();
            LoadReportComments();
            LoadAndSetTotalReports();
        }

        /// <summary>
        /// This method only resolves the reported article.
        /// </summary>
        private void ResolveArticleReport()
        {
            string articleReportID = HiddenReportArticleID.Value.ToString();
            Report Resolve = new Report();
            Resolve.ID = int.Parse(articleReportID);
            //Resolve.articleORcomment_id = commentID;
            Resolve.ModeratorUserID = int.Parse(Session["UserID"].ToString());
            Resolve.ResolveReportedArticle();
        }

        /// <summary>
        /// This method unlocks a reported article in the database. 
        /// Uses article class to perfrom.
        /// </summary>
        private void UnlockArticle()
        {
            Articles unlock = new Articles();
            unlock.ID = int.Parse(HiddenArticleIDBox.Value);       
            unlock.Locked = false;
            unlock.UnlockArticle();
        }

        /// <summary>
        /// This method reverts to an older version of an article.
        /// Collects said copy from database and updates article table
        /// with said copy.
        /// </summary>
        /// <param name="articleCopyID"></param>
        /// <returns></returns>
        private bool RevertArticle(int articleCopyID)
        {
            bool locked = false;
            bool ok = false;
            Articles Revert = new Articles();
            DataTable dt = Revert.GetArticleCopy(articleCopyID);

            Revert.ID = int.Parse(dt.Rows[0]["article_id"].ToString());
            Revert.Title = dt.Rows[0]["title"].ToString();
            Revert.Text = dt.Rows[0]["text"].ToString();
            Revert.AricleURL = dt.Rows[0]["url"].ToString();
            Revert.TypeOfOrg_id = int.Parse(dt.Rows[0]["orgtype_id"].ToString());
            Revert.lastEdit = DateTime.Parse(dt.Rows[0]["lastedit_date"].ToString());
            Revert.lastEditUser_id = int.Parse(dt.Rows[0]["lastedituser_id"].ToString());
            Revert.Publisher = dt.Rows[0]["publisher"].ToString();
            Revert.domainOwner = dt.Rows[0]["domainowner"].ToString();
            Revert.Financing = dt.Rows[0]["financing"].ToString();
            Revert.Reference = dt.Rows[0]["reference_xml"].ToString();
            Revert.Locked = locked;

            if(Revert.UpdateArticle())
            {
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Event for when user click buttons. Shows reported coment. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnShowComments_Click(object sender, EventArgs e)
        {
            lblSection.Text = "Reported Comments";
            ArticleBox.Visible = false;
            HideArticlesReports();
            LoadReportComments();
        }

        /// <summary>
        /// Event for item command when users clicks button in listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void listViewReports_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNamevalue = e.CommandName.ToString();
            HiddenField HiddenReportCommentID = (HiddenField)e.Item.FindControl("HiddenReportCommentID");
            HiddenField HiddenCommentID = (HiddenField)e.Item.FindControl("HiddenCommentID");
            int reportcommentID = int.Parse(HiddenReportCommentID.Value.ToString());
            int commentID = int.Parse(HiddenCommentID.Value.ToString());
            switch (CommandNamevalue)
            {
                case "DeleteComment":
                    DeleteComment(reportcommentID, commentID);
                    break;
                case "NoAction":
                    ResolveComment(reportcommentID, commentID);
                    break;
            }
        }

        /// <summary>
        /// Event for when user clicks to see reported articles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnShowArticles_Click(object sender, EventArgs e)
        {
            lblSection.Text = "Reported Articles";
            HideCommentsReports();
            ArticleBox.Visible = false;
            LoadReportedArticles();
        }

        /// <summary>
        /// Event for item command when users clicks a button in listview for articles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListViewReportArticles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNameValue = e.CommandName.ToString();
            HiddenField HiddenArticleIDValue = (HiddenField)e.Item.FindControl("HiddenArticleID");
            HiddenField HiddenReportArtileID = (HiddenField)e.Item.FindControl("HiddenArticleReportIDListView");
            string articleID = HiddenArticleIDValue.Value.ToString();
            string reportArticleID = HiddenReportArtileID.Value.ToString();
            switch (CommandNameValue)
            {
                case "ShowDetails":
                    LoadArticle(articleID, reportArticleID);
                    break;
            }
        }

        /// <summary>
        /// Event for when user clicks to edit an article.
        /// User is then being redirected to edit page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnEditArticle_Click(object sender, EventArgs e)
        {
            ResolveArticleReport();
            Session["ArticleID"] = HiddenArticleIDBox.Value;
            Response.Redirect("~/EditArticle.aspx");
        }

        /// <summary>
        /// Event for when a moderator clicks button after deciding that the report is unnecessary
        /// and article content does not violate community standars.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnNoActionArticle_Click(object sender, EventArgs e)
        {
            ResolveArticleReport();
            LoadReportedArticles();
            LoadAndSetTotalReports();
            UnlockArticle();
            HideCommentsReports();
            ArticleBox.Visible = false;
        }

        /// <summary>
        /// Event for when a user clicks the revert button to revert to an older version of 
        /// an article, shows dropdown to be able to choose what verision and hides buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnRevertArticle_Click(object sender, EventArgs e)
        {
            dropDownDiv.Visible = true;
            lBtnEditArticle.Visible = false;
            lBtnNoActionArticle.Visible = false;
            lBtnRevertArticle.Visible = false;
            lBtnConfirmRevert.Visible = true;
            lBtnCancelRevert.Visible = true;
            BindDropDownOldVersions();
        }

        /// <summary>
        /// Event for when users selects something in the dropdown. 
        /// Calls other methods to dispaly older version of article.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropDownOldVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownOldVersion.SelectedIndex > 0)
            {
                int articleCopyID = int.Parse(dropDownOldVersion.SelectedValue);
                LoadArticleCopy(articleCopyID);
            }
        }

        /// <summary>
        /// Event for when a user wants to cancel revert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnCancelRevert_Click(object sender, EventArgs e)
        {
            HideCommentsReports();
            ArticleBox.Visible = false;
            dropDownDiv.Visible = false;

            lBtnEditArticle.Visible = true;
            lBtnNoActionArticle.Visible = true;
            lBtnRevertArticle.Visible = true;

            lBtnConfirmRevert.Visible = false;
            lBtnCancelRevert.Visible = false;

            LoadReportedArticles();
        }

        /// <summary>
        /// Event for when a user confirms revert to an onlder version of an article.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnConfirmRevert_Click(object sender, EventArgs e)
        {
            if(dropDownOldVersion.SelectedIndex > 0)
            {
                int articleCopyID = int.Parse(dropDownOldVersion.SelectedValue);

                //UnlockArticle();
                if(!RevertArticle(articleCopyID))
                {
                    lblFail.Text = "Something went wrong when trying to revert. Try again.";
                    lblFail.Visible = true;
                }
                else
                {
                    ResolveArticleReport();

                    LoadReportedArticles();
                    LoadAndSetTotalReports();

                    ArticleBox.Visible = false;
                    dropDownDiv.Visible = false;

                    lBtnEditArticle.Visible = true;
                    lBtnNoActionArticle.Visible = true;
                    lBtnRevertArticle.Visible = true;

                    lBtnConfirmRevert.Visible = false;
                    lBtnCancelRevert.Visible = false;

                }
            }
        }
    }
}