using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.ARTICLE;
using ITSR.CLASSES.USER;

namespace ITSR
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRemovedResolvedCommentReports();
            }
        }

        public void LoadRemovedResolvedCommentReports()
        {
            Report r = new Report();
            lvSolvedCommentReports.DataSource = r.GetRemovedResolvedCommentReports();
            lvSolvedCommentReports.DataBind();
            
        }

        public void UndoRemovedResolvedReport(int commentID, int reportcommentID)
        {
            Report r = new Report();
            r.articleORcomment_id = commentID;
            r.ID = reportcommentID;
            r.BringBackRemovedComment(r);
            LoadRemovedResolvedCommentReports();
        }        

        public void LoadModerators()
        {
            Administrator a = new Administrator();
            lvModerators.DataSource = a.GetAllModerators();
            lvModerators.DataBind();

        }

        protected void lvSolvedCommentReports_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNamevalue = e.CommandName.ToString();
            HiddenField HiddenCommentID = (HiddenField)e.Item.FindControl("HiddenCommentID");
            HiddenField HiddenReportCommentID = (HiddenField)e.Item.FindControl("HiddenReportCommentID");

            int commentID = Convert.ToInt32(HiddenCommentID.Value);
            int reportcommentID = Convert.ToInt32(HiddenReportCommentID.Value);
            switch (CommandNamevalue)
            {
                case "UndoRemovedResolvedReport":
                        UndoRemovedResolvedReport(commentID, reportcommentID);                                  
                    break;

            }
        }       

        protected void lvSolvedCommentReports_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblAction = (Label)e.Item.FindControl("lblAction");

            if (lblAction.Text == "1")
            {
                lblAction.Text = "Deleted";
            }
            else if(lblAction.Text == "0")
            {
                lblAction.Text = "No action";
            }
        }

        protected void lbShowComments_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "showHistoryBox", "showHistoryBox();", true);
            LoadRemovedResolvedCommentReports();
        }

        protected void lbShowRoles_Click(object sender, EventArgs e)
        {
            LoadModerators();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "showRolesBox", "showRolesBox();", true);
        }
    }
}