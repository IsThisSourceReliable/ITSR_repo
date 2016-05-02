using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSR.CLASSES.USER;

namespace ITSR
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Member m = new Member();
            m.userName = tbUserName.Text;
            m.Email = tbEmail.Text;
            m.Password = tbPassword.Text;
            m.role_id = 1;
            m.certifedUser = false;      

            bool emailExists = m.CheckEmail(m);
            bool userNameExists = m.CheckUserNameExists(m);

            if (userNameExists == true)
            {
                CustomValidator1.IsValid = false;
            }

            if(emailExists == true)
            {
                CustomValidator2.IsValid = false;
            }

            if (emailExists == false && userNameExists == false)
            {
                Password p = new Password();
                p.PasswordInput = m.Password;
                m.Password = p.CreateSecurePassword();
                m.CreateUser(m);
                Cleartbs();                
            }
        }

        public void Cleartbs()
        {
            tbUserName.Text = "";
            tbEmail.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
        }
    }
}