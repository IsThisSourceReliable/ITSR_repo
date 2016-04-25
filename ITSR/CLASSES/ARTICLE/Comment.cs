using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;

namespace ITSR.CLASSES.ARTICLE
{
    public class Comment
    {
        public int ID { get; set; }
        public string text { get; set; }
        public int article_id { get; set; }
        public int user_id { get; set; }
        public bool removed { get; set; }
        public DateTime date { get; set; }

        }
    }
}