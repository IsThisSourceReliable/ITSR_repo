using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.ARTICLE
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int TypeOfOrg_id { get; set; }
        public DateTime lastEdit { get; set; }
        public int upVotes { get; set; }
        public int downVotes { get; set; }
        public int createUser_id { get; set; }
        public string Publisher { get; set; }
        public string domainOwner { get; set; }
        public string financing { get; set; }
    }
}