using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;
using MySql.Data.MySqlClient;
using System.Data;

namespace ITSR.CLASSES.ARTICLE
{
    public class Report
    {
        public int ID { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public int articleORcomment_id { get; set; }
        public int ModeratorUserID { get; set; }

        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        /// <summary>
        /// Method gets all comments that have been reported by users 
        /// inner join tables with aliases to be able to join user
        /// table twice and get username for different users.
        /// </summary>
        /// <returns></returns>
        public DataTable GetReportedComments()
        {
            string sql = "SELECT idcomment, idreport_comment, reason, comment_text, commentusertable.iduser AS idusercomment, commentusertable.username AS usernamecomment, reportusertable.iduser AS iduserreport, reportusertable.username AS usernamereport FROM report_comment " +
                        "INNER JOIN comment ON idcomment = comment_id " +
                        "INNER JOIN user AS reportusertable ON reportusertable.iduser = report_user_id " +
                        "INNER JOIN user AS commentusertable ON commentusertable.iduser = user_id " +
                        "WHERE removed = 0 AND resolved = 0 ORDER BY idreport_comment ASC; ";

            try
            {
                conn.Open();
                MySqlCommand cmdGetReportComments = new MySqlCommand(sql, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();

                da.SelectCommand = cmdGetReportComments;

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
        /// This method checks if there is a report on a ceratin comment, returns true 
        /// or false depending on if there is a report or not.
        /// </summary>
        /// <returns></returns>
        public bool CheckReportExists()
        {
            string sql = "SELECT * FROM report_comment WHERE comment_id = @commentid";
            bool exists = false;
            try
            {
                conn.Open();
                MySqlCommand cmdCheckReport = new MySqlCommand(sql, conn);
                cmdCheckReport.Parameters.AddWithValue("@commentid", articleORcomment_id);
                MySqlDataAdapter da = new MySqlDataAdapter();

                da.SelectCommand = cmdCheckReport;

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    exists = true;
                }
                return exists;
            }
            catch (MySqlException ex)
            {
                return exists;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Method counts all comment reports where they haven't
        /// been resolved yet.
        /// </summary>
        /// <returns></returns>
        public int GetTotalCommentsReports()
        {
            string sql = "SELECT count(*) FROM report_comment WHERE resolved = 0;";
            int total = 0;

            try
            {
                conn.Open();
                MySqlCommand cmdCountReportsComment = new MySqlCommand(sql, conn);

                total = Convert.ToInt32(cmdCountReportsComment.ExecuteScalar());
                return total;

            }
            catch (MySqlException ex)
            {
                return total;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Method removes a reported comment.
        /// Updates report_comment table with resolved value and moderator id
        /// Updates comment table with removed value and moderator id
        /// </summary>
        public void RemoveReportedComment()
        {
            string sqlUpdateReportComment = "UPDATE report_comment SET resolved = @resolved, moderator_user_id = @moderatorid WHERE comment_id = @idcomment";
            string sqlUpdateCommentRemoved = "UPDATE comment SET removed = @removed, removed_by_mod_id = @moderatorid WHERE idcomment = @idcomment";
            bool resolved = true;
            bool removed = true;

            try
            {
                conn.Open();

                MySqlCommand cmdUpdateReportComment = new MySqlCommand(sqlUpdateReportComment , conn);
                MySqlCommand cmdUpdateCommentRemoved = new MySqlCommand(sqlUpdateCommentRemoved , conn);

                cmdUpdateReportComment.Parameters.AddWithValue("@resolved", resolved);
                cmdUpdateReportComment.Parameters.AddWithValue("@moderatorid", ModeratorUserID);
                cmdUpdateReportComment.Parameters.AddWithValue("@idcomment", articleORcomment_id);

                cmdUpdateCommentRemoved.Parameters.AddWithValue("@removed", removed);
                cmdUpdateCommentRemoved.Parameters.AddWithValue("@moderatorid", ModeratorUserID);
                cmdUpdateCommentRemoved.Parameters.AddWithValue("@idcomment", articleORcomment_id);

                cmdUpdateReportComment.ExecuteNonQuery();
                cmdUpdateCommentRemoved.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Method updates report_comment when a moderator decides that a report 
        /// is not justified and no action is to be done.
        /// Updates report_comment with resolved value.
        /// </summary>
        public void ResolveReportedComment()
        {
            string sqlUpdateReportComment = "UPDATE report_comment SET resolved = @resolved, moderator_user_id = @moderatorid WHERE comment_id = @idcomment";
            bool resolved = true;

            try
            {
                conn.Open();

                MySqlCommand cmdUpdateReportComment = new MySqlCommand(sqlUpdateReportComment, conn);

                cmdUpdateReportComment.Parameters.AddWithValue("@resolved", resolved);
                cmdUpdateReportComment.Parameters.AddWithValue("@moderatorid", ModeratorUserID);
                cmdUpdateReportComment.Parameters.AddWithValue("@idcomment", articleORcomment_id);

                cmdUpdateReportComment.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}