using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.USER;

namespace ITSR
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                HideLeftMenuButtons();
                RightMenuLogOut.Visible = false;
            }
            else if(Session["UserID"] != null)
            {
                ShowLeftMenuButtons();
                SetLoginGUI();

                if(int.Parse(Session["RoleID"].ToString()) >= 2)
                {
                    ShowModeratorBtn();
                }
            }

            if(!IsPostBack)
            {
                lblUserPassWrong.Visible = false;

            }
        }

        /// <summary>
        /// Both methods below are events to be applied in articlepage to update contentpage.
        /// </summary>
        public delegate void SomeThingHappened();
        public event SomeThingHappened UpdateArticlePage;

        /// <summary>
        /// Methods hides menu items in left menu.
        /// </summary>
        private void HideLeftMenuButtons()
        {
            EditProfileLink.Visible = false;
            MyProfileLink.Visible = false;
            CreateSourceLink.Visible = false;
            ModPanelLink.Visible = false;
        }

        /// <summary>
        /// Method shows menu items in left menu.
        /// </summary>
        private void ShowLeftMenuButtons()
        {
            EditProfileLink.Visible = true;
            MyProfileLink.Visible = true;
            CreateSourceLink.Visible = true;
        }

        private void ShowModeratorBtn()
        {
            ModPanelLink.Visible = true;
        }

        /// <summary>
        /// Method sets error messages when user gives wrong username or password.
        /// </summary>
        private void SetErrorMessage()
        {
            lblUserPassWrong.Text = "Wrong username or password";
            lblUserPassWrong.Visible = true;
            RightMenuLbl.InnerText = "1";
        }
        
        /// <summary>
        /// Method sets the GUI to values that are to be either shown or hidden when a user 
        /// logs in.
        /// </summary>
        private void SetLoginGUI()
        {
            lblUserPassWrong.Text = "Login sucess!";
            lblUserPassWrong.Visible = true;
            RightMenu.Visible = false;
            RightMenuLogOut.Visible = true;
        }

        /// <summary>
        /// Method closes login menu from code behind using javascript in the front end.
        /// </summary>
        private void CloseLoginMenu()
        {
            RightMenuLbl.InnerText = "1";
            //Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "RightMenuOpenClose", "RightMenuOpenClose()", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "RightMenuOpenClose", "RightMenuOpenClose();", true);
        }
        /// <summary>
        /// CLick event for btnLogin, event evaluates that a user first exists then 
        /// check the password is corret. Uses classes USER and PASSWORD to perfom.
        /// Method also sets a Session with the users id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Member MemberLogin = new Member();
            MemberLogin.userName = txtUsername.Text;
            MemberLogin.Password = txtPassword.Text;


            if (MemberLogin.CheckUserNameExists())
            {
                if(MemberLogin.TryLogin())
                {
                    Session["UserID"] = MemberLogin.ID.ToString();
                    Session["RoleID"] = MemberLogin.role_id.ToString();

                    SetLoginGUI();
                    ShowLeftMenuButtons();
                    if (int.Parse(Session["RoleID"].ToString()) >= 2)
                    {
                        ShowModeratorBtn();
                    }
                    CloseLoginMenu();
                    CheckPagePath();
                }
                else
                {
                    SetErrorMessage();
                }
            }
            else
            {
                SetErrorMessage();
            }
        }

        private void CheckPagePath()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            if (path == "/Article.aspx")
            {
                UpdateArticlePage();
            }            
        }

        /// <summary>
        /// Event for linkbutton to log out. Clears all the sessions and redirects to default page. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lBtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/default.aspx");
        }

        protected void linkBtnJoin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

        protected void lBtnHomeLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        protected void lBtnAboutLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/About.aspx");
        }

        protected void lBtnMyProfileLink_Click(object sender, EventArgs e)
        {
            Session["profileID"] = Session["userID"];
            Response.Redirect("~/Profile.aspx");
        }

        protected void lBtnEditProfileLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EditProfile.aspx");
        }

        protected void lBtnCreateArticle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CreateArticle.aspx");
        }

        protected void lBtnMoedPanelLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ModPanel.aspx");
        }
    }
}