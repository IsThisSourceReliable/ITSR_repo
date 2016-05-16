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
            LoadUser(21);
        }

        public void LoadUser(int userid)
        {
            Member m = new Member();

            m.ID = userid;

            m.LoadUser(m);
            m.LoadUserProfile(m);
            FillTxtBoxes(m);
        }

        public void FillTxtBoxes(User u)
        {
            tbUserName.Text = u.userName;
            tbEmail.Text = u.Email;
            tbPassword.Text = u.Password;
            tbConfirmPassword.Text = u.Password;

            tbFirstName.Text = u.firstname;
            tbLastName.Text = u.lastName;
            tbCountry.Text = u.country;
            tbLocation.Text = u.location;
            tbOccupation.Text = u.occupation;
            tbAboutMe.Text = u.aboutme;
        }
    }
}