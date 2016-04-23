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


        //Methods
        public void BanUser(BlockBan b)
        {
            string sql = "INSERT INTO banned_user (user_id) VALUES(@UID)";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UID", b.user_id);

                cmd.ExecuteNonQuery();
           
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public void BlockUser(BlockBan b)
        {

            bool exists = b.CheckIfBlockedBefore(b);

            string sql = "INSERT INTO blocked_user (user_id, count) VALUES(@UID, @C)";

            try
            {
                if (exists)
                {
                    sql = "Update blocked_user SET count = count + 1 WHERE user_id = @UID";
                }

                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UID", b.user_id);
                //cmd.Parameters.AddWithValue("@FD", b.fromDate);
                //cmd.Parameters.AddWithValue("@TD", b.toDate);
                if (exists == false)
                {
                    cmd.Parameters.AddWithValue("@C", 0);
                }

                cmd.ExecuteNonQuery();
                
            }
            catch (MySqlException ex)
            {
                
            }
            finally
            {
                
                conn.Close();
            }

            int count = CountBlock(b);
            if(count >= 3)
            {
                BanUser(b);
            }
        }
        public bool CheckIfBlockedBefore(BlockBan b)
        {
            string sql = "SELECT EXISTS(SELECT 1 FROM blocked_user WHERE user_id = @UID)";

            bool exists = false;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UID", b.user_id);
                exists = Convert.ToBoolean(cmd.ExecuteScalar());
                return exists;
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return exists;
        }
        public int CountBlock(BlockBan b)
        {
            string sql = "SELECT count FROM blocked_user WHERE user_id = @UID";

            int count = 0;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UID", b.user_id);
                count = Convert.ToInt16(cmd.ExecuteScalar());
                return count;
            }
            catch (MySqlException ex)
            {
                return count;
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}