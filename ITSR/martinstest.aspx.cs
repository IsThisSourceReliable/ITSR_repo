using ITSR.CLASSES.USER;
using ITSR.CLASSES.ARTICLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITSR

{
    public partial class martinstest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Member m = new Member();
            m.userName = "Myrtass";
            m.Password = "Test";
            m.role_id = 4;
            m.Email = "Myrtass@gmail.com";
            m.certifedUser = true;

            m.CreateUser(m);

            Vote v = new Vote();

            v.user_id = 
        }
    }
}