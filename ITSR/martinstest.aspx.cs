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
            BlockBan b = new BlockBan();

            b.fromDate = DateTime.Today;
            b.toDate = DateTime.Today.AddDays(1);
            b.user_id = 21;

           b.BlockUser(b);
        }
    }
}