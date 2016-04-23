using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;

namespace ITSR.CLASSES.ARTICLE
{
    public class Report
    {
        public int ID { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public int articleORcomment_id { get; set; }

        //Methods
        public void InsertCommentReport()
        {

        }
        public void InsertArticleReport()
        {

        }
    }
}