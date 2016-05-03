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
        public string Reference { get; set; }
        public string AricleURL { get; set; }

        //Methods

        /// <summary>
        /// Method inserts an arcitle into the database and returns true or false depending if succesful
        /// or not.
        /// </summary>
        /// <returns></returns>
        public bool CreateArticle()
        {
            try
            {
                conn.Open();

                MySqlCommand cmdCreateArticle = new MySqlCommand("INSERT INTO article (title, text, url, orgtype_id, lastedit_date, votes_up, votes_down, lastedituser_id, createuser_id, publisher, domainowner, financing, reference_xml) " +
                                                                               "VALUES(@title, @text, @url, @typeoforg, @lasteditdate, @upvotes, @downvotes, @lastedituser, @createuserid, @publisher, @domainowner, @financer, @referencexml);", conn);

                cmdCreateArticle.Parameters.AddWithValue("@title", Title);
                cmdCreateArticle.Parameters.AddWithValue("@text", Text);
                cmdCreateArticle.Parameters.AddWithValue("@url", AricleURL);
                cmdCreateArticle.Parameters.AddWithValue("@typeoforg", TypeOfOrg_id);
                cmdCreateArticle.Parameters.AddWithValue("@lasteditdate", lastEdit);
                cmdCreateArticle.Parameters.AddWithValue("@upvotes", upVotes);
                cmdCreateArticle.Parameters.AddWithValue("@downvotes", downVotes);
                cmdCreateArticle.Parameters.AddWithValue("@lastedituser", lastEditUser_id);
                cmdCreateArticle.Parameters.AddWithValue("@createuserid", createUser_id);
                cmdCreateArticle.Parameters.AddWithValue("@publisher", Publisher);
                cmdCreateArticle.Parameters.AddWithValue("@domainowner", domainOwner);
                cmdCreateArticle.Parameters.AddWithValue("@financer", Financing);
                cmdCreateArticle.Parameters.AddWithValue("@referencexml", Reference);

                cmdCreateArticle.ExecuteNonQuery();
                return true;

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
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

        public DataTable LoadArticleComments()
        {
            string sql = "SELECT * FROM comment WHERE article_id = @AID";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AID", ID);
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

        public DataTable SearchForSpecificArticle(string SearchString)
        {
            string sql = "SELECT * FROM article WHERE title = @SS OR url = @SS";

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SS", SearchString);
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

        public DataTable SearchForUnspecificArticle(string SearchString)
        {
            string SS = "%" + SearchString + "%";
            string sql = "SELECT * FROM article WHERE title LIKE @SS OR url LIKE @SS";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SS", SS);
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

        public int SearchResultCount(string SearchString)
        {
            string sql = "SELECT COUNT(*) FROM article WHERE title = @SS OR url = @SS2";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@SS", SearchString);
                int count = Convert.ToInt16(cmd.ExecuteScalar());
                return count;
            }
            catch
            {
                return 0;
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

        /// <summary>
        /// Method calculates the totalvotes on a article.
        /// </summary>
        /// <returns></returns>
        public int SetTotalVotes()
        {
            int totalVotes = upVotes + downVotes;
            return totalVotes;
        }


        /// <summary>
        /// Method calculates the percent of upvotes using the amount of totalvotes.
        /// </summary>
        /// <param name="totalVotes"></param>
        /// <returns></returns>
        public double SetUpVotePercent(int totalVotes)
        {         
            if(totalVotes == 0)
            {
                return 0;
            }
            else
            {
                double pointPercent = Convert.ToDouble(upVotes) / Convert.ToDouble(totalVotes);

                double daPercent = pointPercent * 100;

                double dazPercent = Math.Round(daPercent, MidpointRounding.AwayFromZero);

                return dazPercent;
            }
        }


        /// <summary>
        /// Method uses the upvotpercent to calculate the percent for the downvotes.
        /// </summary>
        /// <param name="upVotePercent"></param>
        /// <returns></returns>
        public int SetDownVotesPercent(double upVotePercent)
        {
            if(upVotePercent == 0)
            {
                return 0;
            }
            else
            {
                int downVotePercent = 100 - Convert.ToInt32(upVotePercent);
                return downVotePercent;
            }
        }


        /// <summary>
        /// Method returns a datatable contain all the different types of organisations
        /// that can be found in the database.
        /// </summary>
        /// <returns></returns>
        public DataTable GetTypeOfOrgs()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM typeoforg; ", conn);
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

        //public int GetUpVotes(Articles a)
        //{
        //    string sql = "SELECT votes_up FROM article WHERE idarticle = @AID";

        //    try
        //    {
        //        conn.Open();

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@AID", a.ID);

        //        int UpVotes = Convert.ToInt32(cmd.ExecuteScalar());

        //        return upVotes;

        //    }
        //    catch (MySqlException ex)
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //}
        //public int GetDownVotes(Articles a)
        //{
        //    string sql = "SELECT votes_down FROM article WHERE idarticle = @AID";

        //    try
        //    {
        //        conn.Open();

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@AID", a.ID);

        //        int downVotes = Convert.ToInt32(cmd.ExecuteScalar());

        //        return downVotes;

        //    }
        //    catch (MySqlException ex)
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //}
        //public int GetTotalVotes(Articles a)
        //{
        //    int downVotes = GetDownVotes(a);
        //    int upVotes = GetUpVotes(a);
        //    int totalVotes = upVotes + downVotes;
        //    return totalVotes;
        //}
    }
}