using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;

namespace ITSR.CLASSES.ARTICLE
{  
    public class Articles
    {
        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int TypeOfOrg_id { get; set; }
        public DateTime lastEdit { get; set; }
        public int lastEditUser_id { get; set; }
        public int upVotes { get; set; }
        public int downVotes { get; set; }
        public int createUser_id { get; set; }
        public string Publisher { get; set; }
        public string domainOwner { get; set; }
        public string Financing { get; set; }

        public int GetUpVotes(Articles a)
        {
            string sql = "SELECT votes_up FROM article WHERE idarticle = @AID";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@AID", a.ID);

                int UpVotes = Convert.ToInt32(cmd.ExecuteScalar());

                return upVotes;
            
            }
            catch(MySqlException ex)
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }
          
        }
        public int GetDownVotes(Articles a)
        {
            string sql = "SELECT votes_down FROM article WHERE idarticle = @AID";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@AID", a.ID);

                int downVotes = Convert.ToInt32(cmd.ExecuteScalar());

                return downVotes;

            }
            catch (MySqlException ex)
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }
            
        }
        public int GetTotalVotes(Articles a)
        {
            int downVotes = GetDownVotes(a);
            int upVotes = GetUpVotes(a);
            int totalVotes = upVotes + downVotes;
            return totalVotes;
        }
    }
}