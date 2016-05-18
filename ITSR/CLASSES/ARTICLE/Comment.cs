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
        public string ReportReason { get; set; }
        public string Order { get; set; }
        public int Limit { get; set; }

        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        /// <summary>
        /// Method inserts comment to database.
        /// </summary>
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

        /// <summary>
        /// Method sets status on comment to correct status. To fit in with 
        /// tinyint in database table. 
        /// </summary>
        /// <returns></returns>
        private int SetRemovedStatus()
        {
            int newRemoved = 0;

            if (Removed == true)
            {
                newRemoved = 1;
            }

            return newRemoved;
        }

        /// <summary>
        /// Method gets comment on a certain article. How many and which order depends on user values.
        /// </summary>
        /// <returns></returns>
        public DataTable GetComments()
        {
            string sql = SetSqlOrder();
            try
            {
                conn.Open();
                MySqlCommand cmdGetComments = new MySqlCommand(sql, conn);

                cmdGetComments.Parameters.AddWithValue("@articleid", Article_id);
                cmdGetComments.Parameters.AddWithValue("@limit", Limit);

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

        /// <summary>
        /// Method sets sql question in order to show latest or first comments
        /// </summary>
        /// <returns></returns>
        private string SetSqlOrder()
        {
            string sql = string.Empty;

            if (Order == "First")
            {
                sql = "SELECT idcomment, comment_text, user_id, article_id, date, removed, iduser, username FROM comment " +
                                                    "INNER JOIN user ON user_id = iduser " +
                                                    "WHERE article_id = @articleid ORDER BY idcomment ASC LIMIT @limit; ";
            }
            else
            {
                sql = "SELECT idcomment, comment_text, user_id, article_id, date, removed, iduser, username FROM comment " +
                                    "INNER JOIN user ON user_id = iduser " +
                                    "WHERE article_id = @articleid ORDER BY idcomment DESC LIMIT @limit; ";
            }

            return sql;
        }

        /// <summary>
        /// Method counts totalt amount of comments on a comment. Try to include this count 
        /// function in getcomments sql question instead perhaps?
        /// </summary>
        /// <returns></returns>
        public DataTable CountComments()
        {
            conn.Open();
            try
            {
                MySqlCommand cmdGetComments = new MySqlCommand("SELECT count(idcomment) as totalcomments  FROM comment " +
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

        /// <summary>
        /// Method inserts in to datatable when a user reports a comment.
        /// </summary>
        public void ReportComment()
        {
            try
            {
                conn.Open();

                MySqlCommand cmdReportComment = new MySqlCommand("INSERT INTO report_comment (comment_id, reason, report_user_id) " +
                                                                "VALUES(@commentid, @reason, @userid);", conn);

                cmdReportComment.Parameters.AddWithValue("@commentid", ID);
                cmdReportComment.Parameters.AddWithValue("@reason", ReportReason);
                cmdReportComment.Parameters.AddWithValue("@userid", User_id);

                cmdReportComment.ExecuteNonQuery();

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
    }
}