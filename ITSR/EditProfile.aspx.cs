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
            tbUserName.Text = m.userName;
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
            m.userName = tbUserName.Text;
            m.Email = tbEmail.Text;

            m.firstname = tbFirstName.Text;
            m.lastName = tbLastName.Text;
            m.country = tbCountry.Text;
            m.location = tbLocation.Text;
            m.occupation = tbOccupation.Text;
            m.aboutme = tbAboutMe.Text;

            m.UpdateUser(m);
            m.UpdateUserProfile(m);
           
        }

        protected void BtnNewPassword_Click(object sender, EventArgs e)
        {
            
        }
    }
}