using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.USER
{
    public class BlockBan
    {
        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        public int ID { get; set; }
        public int user_id { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int Count { get; set; }
    }
}