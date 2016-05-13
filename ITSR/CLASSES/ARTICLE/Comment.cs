using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITSR.CLASSES.ARTICLE
{
    public class Comment
    {
        public int ID { get; set; }
        public string CommentText { get; set; }
        public int Article_id { get; set; }
        public int User_id { get; set; }
        public bool Removed { get; set; }
        public DateTime Date { get; set; }

        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        public void InsertComment()
        {
            try
            {
                int newRemoved = SetRemovedStatus();
                conn.Open();

                MySqlCommand cmdInsertComment = new MySqlCommand("INSERT INTO comment (comment_text, user_id, article_id, date, removed) " +
                                                                               "VALUES(@commenttext, @userid, @articleid, @date, @removed);", conn);

                cmdInsertComment.Parameters.AddWithValue("@commenttext", CommentText);
                cmdInsertComment.Parameters.AddWithValue("@userid", User_id);
                cmdInsertComment.Parameters.AddWithValue("@articleid", Article_id);
                cmdInsertComment.Parameters.AddWithValue("@date", Date);
                cmdInsertComment.Parameters.AddWithValue("@removed", newRemoved);

                cmdInsertComment.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private int SetRemovedStatus()
        {
            int newRemoved = 0;

            if (Removed == true)
            {
                newRemoved = 1;
            }

            return newRemoved;
        }

        public DataTable GetComments()
        {
            conn.Open();
            try
            {
                MySqlCommand cmdGetComments = new MySqlCommand("SELECT idcomment, comment_text, user_id, article_id, date, removed, iduser, username FROM comment " +
                                                    "INNER JOIN user ON user_id = iduser " +
                                                    "WHERE article_id = @articleid; ", conn);

                cmdGetComments.Parameters.AddWithValue("@articleid", Article_id);

                MySqlDataAdapter da = new MySqlDataAdapter();

                da.SelectCommand = cmdGetComments;

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