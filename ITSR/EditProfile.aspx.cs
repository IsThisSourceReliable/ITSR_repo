using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.USER;

namespace ITSR
{
    public partial class EditProfile : System.Web.UI.Page
    {

        //Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userID"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    LoadUser(Convert.ToInt32(Session["userID"]));
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            UpdateUser();           
        }
        protected void BtnNewPassword_Click(object sender, EventArgs e)
        {

            UpdatePassWord();
            
        }


        //Methods
        public void LoadUser(int userid)
        {
            Member m = new Member();

            m.ID = userid;

            m.LoadUser(m);
            m.LoadUserProfile(m);
            FillTxtBoxes(m);
        }
        public void FillTxtBoxes(Member m)
        {                     
            tbEmail.Text = m.Email;

            tbFirstName.Text = m.firstname;
            tbLastName.Text = m.lastName;
            tbCountry.Text = m.country;
            tbLocation.Text = m.location;
            tbOccupation.Text = m.occupation;
            tbAboutMe.Text = m.aboutme;
        }
        public void UpdateUser()
        {
            Member m = new Member();

            m.ID = Convert.ToInt32(Session["userID"]);
            m.Email = tbEmail.Text;

            m.firstname = tbFirstName.Text;
            m.lastName = tbLastName.Text;
            m.country = tbCountry.Text;
            m.location = tbLocation.Text;
            m.occupation = tbOccupation.Text;
            m.aboutme = tbAboutMe.Text;

            if (!m.CheckEmail(m, true))
            {
                if (m.UpdateUser(m) && m.UpdateUserProfile(m))
                {
                    ConfirmedValidator.IsValid = false;
                }
                else
                {
                    SomethingWrongVal.IsValid = false;
                }
                
                LoadUser(Convert.ToInt32(Session["userID"]));
            }
            else
            {
                CustomValidator2.IsValid = false;
            }


            

        }
        public void UpdatePassWord()
        {
            Member m = new Member();
            Password p = new Password();
            m.ID = Convert.ToInt32(Session["userID"]);

            p.PasswordInput = tbOldPassword.Text;
            p.user_id = m.ID;
            if (p.TryPassword())
            {
                m.Password = tbPassword.Text;
                p.PasswordInput = m.Password;
                m.Password = p.CreateSecurePassword();

                m.UpdatePassWord(m);
                EmptyPasswordTextBoxes();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "hideDiv", "hideDiv();", true);
            }
            else
            {
                WrongPassWordValidator.IsValid = false;
            }
            

        }
        public void EmptyPasswordTextBoxes()
        {
            tbOldPassword.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
        }
        public void EmptyTextBoxes()
        {
            tbEmail.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbCountry.Text = "";
            tbLocation.Text = "";
            tbOccupation.Text = "";
            tbAboutMe.Text = "";
            EmptyPasswordTextBoxes();
        }
        
    }
}