using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.ARTICLE;
using ITSR.CLASSES.USER;
using System.Data;

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

        public void LoadRoles()
        {
            Role r = new Role();
            r.GetRoles();
        }

        public void SaveNewRole(Member m, int roleID)
        {
            Administrator a = new Administrator();
            a.UpdateUserRole(m, roleID);
            LoadModerators();
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
            else if (lblAction.Text == "0")
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

        protected void lvModerators_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddlRoles = (DropDownList)e.Item.FindControl("ddlRoles");
            HiddenField HiddenUserID = (HiddenField)e.Item.FindControl("HiddenUserID");
            Button BtnSaveRole = (Button)e.Item.FindControl("BtnSaveRole");

            Member m = new Member();
            m.ID = Convert.ToInt32(HiddenUserID.Value);
            int userLvl = m.GetUserLvl(m.ID);

            Role r = new Role();
            DataTable dtRoles = r.GetRoles();

            foreach (DataRow role in dtRoles.Rows)
            {
                ListItem item = new ListItem();
                item.Text = role["role"].ToString();
                item.Value = role["idrole"].ToString();
                ddlRoles.Items.Add(item);

                if (userLvl > 2)
                {
                    ddlRoles.Enabled = false;
                    BtnSaveRole.Enabled = false;
                }
            }

            foreach (ListItem item in ddlRoles.Items)
            {

                if (Convert.ToInt16(item.Value) == userLvl)
                {
                    item.Selected = true;
                }
            }
        }

        protected void lvModerators_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNamevalue = e.CommandName.ToString();
            HiddenField HiddenUserID = (HiddenField)e.Item.FindControl("HiddenUserID");
            DropDownList ddlRoles = (DropDownList)e.Item.FindControl("ddlRoles");

            Member m = new Member();
            m.ID = Convert.ToInt32(HiddenUserID.Value);
            int roleID = Convert.ToInt16(ddlRoles.SelectedValue);


            switch (CommandNamevalue)
            {
                case "SaveNewRole":
                    SaveNewRole(m, roleID);
                    break;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Member m = new Member();
            DataTable dtSearch = m.SearchUser(tbSearch.Text);

            lvSearchMember.DataSource = dtSearch;
            lvSearchMember.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MakeBtnSmaller", "MakeBtnSmaller();", true);
        }

        protected void lvSearchMember_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string CommandNamevalue = e.CommandName.ToString();
            HiddenField HiddenUserID = (HiddenField)e.Item.FindControl("HiddenUserID");
            HiddenField HiddenField1 = (HiddenField)e.Item.FindControl("HiddenField1");

            Member m = new Member();
            m.ID = Convert.ToInt32(HiddenUserID.Value);


            switch (CommandNamevalue)
            {
                case "MakeModerator":
                    SaveNewRole(m, 2);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MakeBtnBigger", "MakeBtnBigger();", true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "HideBtn", "HideBtn();", true);
                    HiddenField1.Value = "1";
                    lvSearchMember.DataSource = "";
                    lvSearchMember.DataBind();                   
                    break;
            }
        }

        protected void lvSearchMember_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ShowSearchResult", "ShowSearchResult();", true); 
        }
    }
}