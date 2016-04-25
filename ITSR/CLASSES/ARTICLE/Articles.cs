using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;
using System.Data;

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

        //Methods
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
            catch (MySqlException ex)
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

        public int SetTotalVotes()
        {
            int totalVotes = upVotes + downVotes;
            return totalVotes;
        }

        public DataTable LoadAllArticles()
        {
            string sql = "SELECT * FROM article";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);

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
        public DataTable LoadArticleComments(Articles a)
        {

            string sql = "SELECT * FROM comment WHERE article_id = @AID";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AID", a.ID);
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

        /// <summary>
        /// Gets a specific article with a sql question which joins user table and
        /// typeoforg table to get the right values.
        /// </summary>
        /// <returns></returns>
        public DataTable GetArticle()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT idarticle, title, text, orgtype_id, lastedit_date, votes_up, votes_down, lastedituser_id, createuser_id, publisher, domainowner,financing, reference_xml, removed, name AS orgtype, username AS edituser FROM article " +
                                                    "INNER JOIN typeoforg ON article.orgtype_id = typeoforg.idtypeoforg " +
                                                    "INNER JOIN user ON article.lastedituser_id = user.iduser " +
                                                    "WHERE idarticle = @id; ", conn);
                cmd.Parameters.AddWithValue("@id", ID);
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

        public double SetUpVotePercent(int totalVotes)
        {            
            double pointPercent = Convert.ToDouble(upVotes) / Convert.ToDouble(totalVotes);

            double daPercent = pointPercent * 100;

            double dazPercent = Math.Round(daPercent, MidpointRounding.AwayFromZero);

            return dazPercent;
        }

        public int SetDownVotesPercent(double upVotePercent)
        {
            int downVotePercent = 100 - Convert.ToInt32(upVotePercent);
            return downVotePercent;
        }
    }
}