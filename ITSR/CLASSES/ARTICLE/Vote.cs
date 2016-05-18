using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;
using MySql.Data.MySqlClient;
using System.Data;

namespace ITSR.CLASSES.ARTICLE
{
    public class Vote
    {
        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        public int ID { get; set; }
        public bool VoteType { get; set; }
        public int User_id { get; set; }
        public int Article_id { get; set; }

        private string sqlVoteTable { get; set; }
        private string sqlArticleTable { get; set; }

        /// <summary>
        /// Method evaluates firstly if there is a user vote already, if not
        /// calls method to set correct sqlstring. If there is a vote already
        /// method checks if a user is trying to give same vote again (if a user tries to
        /// upvote/Downvote a source that user already has upvoted/downvoted) if not
        /// calls method to set correct sqlstring.
        /// </summary>
        /// <returns></returns>
        public bool SetVote()
        {
            bool ok = false;
            int intVote = 0;
            bool dbVote = false;
            DataTable dt = new DataTable();
            dt = GetUserVote();

            if(dt.Rows.Count > 0)
            {
                intVote = Convert.ToInt32(dt.Rows[0]["vote"].ToString());
                dbVote = SetDBVote(intVote);

                if(dbVote == VoteType)
                {
                    return ok;
                }
                else
                {
                    SetSqlStringUserVoteExists();
                    VoteArticle();
                    ok = true;
                    return ok;
                }
            }
            else
            {
                SetSqlStringNoUserVote();
                VoteArticle();

                ok = true;
                return ok;
            }
        }

        /// <summary>
        /// Method changes tinyint from database to bool
        /// </summary>
        /// <param name="intVote"></param>
        /// <returns></returns>
        private bool SetDBVote(int intVote)
        {
            bool dbVote = false;
            if (intVote != 0)
            {
                dbVote = true;
            }
            return dbVote;
        }

        /// <summary>
        /// Method gets specific user vote and returns a datatable.
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserVote()
        {
            string sql = "SELECT * FROM vote WHERE user_id = @userid AND article_id = @articleid; ";

            try
            {
                MySqlCommand cmdUserVote = new MySqlCommand(sql, conn);
                cmdUserVote.Parameters.AddWithValue("@userid", User_id);
                cmdUserVote.Parameters.AddWithValue("@articleid", Article_id);

                MySqlDataAdapter da = new MySqlDataAdapter();

                da.SelectCommand = cmdUserVote;

                DataTable dt = new DataTable();

                da.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Method sets the correct sqlstring for when a user already has voted on an article
        /// but wants to change said vote.
        /// </summary>
        private void SetSqlStringUserVoteExists()
        {
            if(VoteType)
            {
                sqlVoteTable = "UPDATE vote SET vote = @vote WHERE user_id = @userid AND article_id = @articleid; ";
                sqlArticleTable = "UPDATE article SET votes_up = votes_up + 1, votes_down = votes_down - 1 WHERE idarticle = @articleid; ";
            }
            else
            {
                sqlVoteTable = "UPDATE vote SET vote = @vote WHERE user_id = @userid AND article_id = @articleid; ";
                sqlArticleTable = "UPDATE article SET votes_down = votes_down + 1, votes_up = votes_up - 1 WHERE idarticle = @articleid; ";
            }
        }
        
        /// <summary>
        /// Method sets correct sqlstring when a user is voting on an article for the first time.
        /// </summary>
        private void SetSqlStringNoUserVote()
        {
            if(VoteType)
            {
                sqlVoteTable = "INSERT INTO vote (user_id, article_id, vote) VALUES(@userid, @articleid, @vote); ";
                sqlArticleTable = "UPDATE article SET votes_up = votes_up + 1 WHERE idarticle = @articleid; ";
            }
            else
            {
                sqlVoteTable = "INSERT INTO vote (user_id, article_id, vote) VALUES(@userid, @articleid, @vote); ";
                sqlArticleTable = "UPDATE article SET votes_down = votes_down + 1 WHERE idarticle = @articleid; ";
            }
        }

        /// <summary>
        /// Method executes the vote itself and inserts or updates the database.
        /// </summary>
        private void VoteArticle()
        {
            try
            {
                conn.Open();

                MySqlCommand cmdVoteTable = new MySqlCommand(sqlVoteTable, conn);
                MySqlCommand cmdArticleVote = new MySqlCommand(sqlArticleTable, conn);

                cmdVoteTable.Parameters.AddWithValue("@userid", User_id);
                cmdVoteTable.Parameters.AddWithValue("@articleid", Article_id);
                cmdVoteTable.Parameters.AddWithValue("@vote", VoteType);

                cmdArticleVote.Parameters.AddWithValue("@articleid", Article_id);

                cmdVoteTable.ExecuteNonQuery();
                cmdArticleVote.ExecuteNonQuery();


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