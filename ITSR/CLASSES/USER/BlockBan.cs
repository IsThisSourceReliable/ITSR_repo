using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.USER
{
    public class BlockBan
    {
        public int ID { get; set; }
        public int user_id { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int Count { get; set; }


        //Methods
        public void BanUser()
        {

        }
        public void BlockUser()
        {

        }
    }
}