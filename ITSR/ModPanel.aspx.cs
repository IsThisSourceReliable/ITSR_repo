using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.ARTICLE;
using System.Data;

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
                    LoadReportComments();
                    LoadAndSetTotalReports();
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }

        private void LoadReportComments()
        {
            Report Comments = new Report();
            DataTable dt = Comments.GetReportedComments();
            SetListView(dt);
        }

        private void SetListView(DataTable dt)
        {
            listViewReports.DataSource = dt;
            listViewReports.DataBind();
        }

        private void LoadAndSetTotalReports()
        {
            Report TotalComments = new Report();
            int total = TotalComments.GetTotalCommentsReports();
            lblTotalReports.Text = total.ToString();
        }

        private void DeleteComment(int commentID)
        {
            Report Delete = new Report();
            Delete.articleORcomment_id = commentID;
            Delete.ModeratorUserID = int.Parse(Session["UserID"].ToString());
            Delete.RemoveReportedComment();
            LoadReportComments();
            LoadAndSetTotalReports();
        }

        private void ResolveComment(int commentID)
        {
            Report Resolve = new Report();
            Resolve.articleORcomment_id = commentID;
            Resolve.ModeratorUserID = int.Parse(Session["UserID"].ToString());
            Resolve.ResolveReportedComment();
            LoadReportComments();
            LoadAndSetTotalReports();
        }

        protected void lBtnShowComments_Click(object sender, EventArgs e)
        {

        }

        protected void listViewReports_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNamevalue = e.CommandName.ToString();
            HiddenField HiddenCommentID = (HiddenField)e.Item.FindControl("HiddenCommentID");
            int commentID = int.Parse(HiddenCommentID.Value.ToString());

            switch (CommandNamevalue)
            {
                case "DeleteComment":
                    DeleteComment(commentID);
                    break;
                case "NoAction":
                    ResolveComment(commentID);
                    break;
            }
        }
    }
}