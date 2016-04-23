using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.ARTICLE;
using MySql.Data.MySqlClient;

namespace ITSR.CLASSES.USER
{
    public class Member:User
    {
        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");



        //Methods
        public void CommentOnArticle(Comment c)
        {
            string sql = "INSERT INTO comment (comment_text, user_id, article_id, removed) VALUES(@CT, @UID, @AID, @R)";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@CT", c.text);
                cmd.Parameters.AddWithValue("@UID", c.user_id);
                cmd.Parameters.AddWithValue("@AID", c.article_id);
                cmd.Parameters.AddWithValue("@R", c.removed);

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
        public void UpVoteArticle(Vote v)
        {
            string sql = "INSERT INTO vote (user_id, article_id, vote) VALUES(@UID, @AID, @V)";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UID", v.user_id);
                cmd.Parameters.AddWithValue("@AID", v.article_id);
                cmd.Parameters.AddWithValue("@V", true);

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
        public void DownVoteArticle(Vote v)
        {
            string sql = "INSERT INTO vote (user_id, article_id, vote) VALUES(@UID, @AID, @V)";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UID", v.user_id);
                cmd.Parameters.AddWithValue("@AID", v.article_id);
                cmd.Parameters.AddWithValue("@V", false);

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
        public void ReportArticle()
        {

        }
        public void ReportComment()
        {

        }


    }
}