using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.ARTICLE;
using MySql.Data.MySqlClient;
using System.Data;

namespace ITSR.CLASSES.USER
{
    public class Member:User
    {
        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        //Methods
        public DataTable GetArticlesCreatedBy(User u)
        {
            string sql = "SELECT * FROM article WHERE createuser_id = @userID ORDER BY votes_up desc LIMIT 5";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", u.ID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (MySqlException ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
            
        }
        public DataTable GetLastCommentsBy(User u)
        {
            string sql = "SELECT * FROM comment INNER JOIN article on (comment.article_id = article.idarticle) WHERE comment.user_id = @uID ORDER BY date DESC LIMIT 5";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uID", u.ID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (MySqlException ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable GetLastVotesBy(User u)
        {
            string sql = "SELECT * FROM vote INNER JOIN article on (vote.article_id = article.idarticle) WHERE vote.user_id = @uID ORDER BY idvote DESC LIMIT 5";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uID", u.ID);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (MySqlException ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}