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
            bool ok = false;
            try
            {
                conn.Open();

                MySqlCommand cmdCreateArticle = new MySqlCommand("INSERT INTO article (title, text, url, orgtype_id, lastedit_date, votes_up, votes_down, lastedituser_id, createuser_id, publisher, domainowner, financing, reference_xml) VALUES(@title, @text, @url, @typeoforg, @lasteditdate, @upvotes, @downvotes, @lastedituser, @createuserid, @publisher, @domainowner, @financer, @referencexml);", conn);

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
                ok = true;
                return ok;

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return ok;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Method updates an arcitle with article id 
        /// in the database and returns true or false depending if succesful
        /// or not.
        /// </summary>
        /// <returns></returns>
        public bool UpdateArticle()
        {
            bool ok = false;
            try
            {
                conn.Open();

                MySqlCommand cmdCreateArticle = new MySqlCommand("UPDATE article " +
                                                                "SET title = @title, text = @text, url = @url, orgtype_id = @typeoforg, lastedit_date = @lasteditdate, lastedituser_id = @lastedituser, publisher = @publisher, domainowner = @domainowner, financing = @financer, reference_xml = @referencexml " +
                                                                "WHERE idarticle = @id; ", conn);

                cmdCreateArticle.Parameters.AddWithValue("@id", ID);
                cmdCreateArticle.Parameters.AddWithValue("@title", Title);
                cmdCreateArticle.Parameters.AddWithValue("@text", Text);
                cmdCreateArticle.Parameters.AddWithValue("@url", AricleURL);
                cmdCreateArticle.Parameters.AddWithValue("@typeoforg", TypeOfOrg_id);
                cmdCreateArticle.Parameters.AddWithValue("@lasteditdate", lastEdit);
                cmdCreateArticle.Parameters.AddWithValue("@lastedituser", lastEditUser_id);
                cmdCreateArticle.Parameters.AddWithValue("@publisher", Publisher);
                cmdCreateArticle.Parameters.AddWithValue("@domainowner", domainOwner);
                cmdCreateArticle.Parameters.AddWithValue("@financer", Financing);
                cmdCreateArticle.Parameters.AddWithValue("@referencexml", Reference);

                cmdCreateArticle.ExecuteNonQuery();
                ok = true;
                return ok;

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return ok;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// This method retrieves the last saved article in database and stores a copy in articlecopy table.
        /// </summary>
        /// <returns></returns>
        public bool SaveOldArticle()
        {
            DataTable dt = GetArticle();
            int copyArticleID = int.Parse(dt.Rows[0]["idarticle"].ToString());
            string copyTitle = dt.Rows[0]["title"].ToString();
            string copyText = dt.Rows[0]["text"].ToString();
            string copyUrl = dt.Rows[0]["url"].ToString();
            int copyOrgID = int.Parse(dt.Rows[0]["orgtype_id"].ToString());
            DateTime copyEditDate = DateTime.Parse(dt.Rows[0]["lastedit_date"].ToString());
            int copyLastEditUser = int.Parse(dt.Rows[0]["lastedituser_id"].ToString());
            int copyCreateUser = int.Parse(dt.Rows[0]["createuser_id"].ToString());
            string copyPublisher = dt.Rows[0]["publisher"].ToString();
            string copyDomainOwner = dt.Rows[0]["domainowner"].ToString();
            string copyFinancer = dt.Rows[0]["financing"].ToString();
            string copyRefXml = dt.Rows[0]["reference_xml"].ToString();
            DateTime copyDate = DateTime.Now;

            bool ok = false;
            try
            {
                conn.Open();

                MySqlCommand cmdCopyArticle = new MySqlCommand("INSERT INTO articlecopy (article_id, title, text, url, orgtype_id, lastedit_date, lastedituser_id, createuser_id, publisher, domainowner, financing, reference_xml, copy_date) VALUES(@ID, @title, @text, @url, @typeoforg, @lasteditdate, @lastedituser, @createuserid, @publisher, @domainowner, @financer, @referencexml, @copydate);", conn);

                cmdCopyArticle.Parameters.AddWithValue("@ID", copyArticleID);
                cmdCopyArticle.Parameters.AddWithValue("@title", copyTitle);
                cmdCopyArticle.Parameters.AddWithValue("@text", copyText);
                cmdCopyArticle.Parameters.AddWithValue("@url", copyUrl);
                cmdCopyArticle.Parameters.AddWithValue("@typeoforg", copyOrgID);
                cmdCopyArticle.Parameters.AddWithValue("@lasteditdate", copyEditDate);
                cmdCopyArticle.Parameters.AddWithValue("@lastedituser", copyLastEditUser);
                cmdCopyArticle.Parameters.AddWithValue("@createuserid", copyCreateUser);
                cmdCopyArticle.Parameters.AddWithValue("@publisher", copyPublisher);
                cmdCopyArticle.Parameters.AddWithValue("@domainowner", copyDomainOwner);
                cmdCopyArticle.Parameters.AddWithValue("@financer", copyFinancer);
                cmdCopyArticle.Parameters.AddWithValue("@referencexml", copyRefXml);
                cmdCopyArticle.Parameters.AddWithValue("@copydate", copyDate);

                cmdCopyArticle.ExecuteNonQuery();
                ok = true;
                return ok;

            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return ok;
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

        public void ReportArticle(Report r)
        {
            string sql = "INSERT INTO report_article (article_id, text, user_id) VALUES(@AID, @T, @UID)";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@AID", r.articleORcomment_id);
                cmd.Parameters.AddWithValue("@T", r.text);
                cmd.Parameters.AddWithValue("@UID", r.user_id);

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
                MySqlCommand cmd = new MySqlCommand(sql, conn);
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
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT idarticle, title, text, url, orgtype_id, lastedit_date, votes_up, votes_down, lastedituser_id, createuser_id, publisher, domainowner,financing, reference_xml, removed, name AS orgtype, username AS edituser FROM article " +
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
            if (totalVotes == 0)
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
            if (upVotePercent == 0)
            {
                int downVotePercent = 100 - Convert.ToInt32(upVotePercent);
                return downVotePercent;
            }
            else
            {
                int downVotePercent = 100 - Convert.ToInt32(upVotePercent);
                return downVotePercent;
            }
        }

        public DataTable GetArticleVotes()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT votes_up, votes_down FROM article WHERE idarticle = @articleid;", conn);
                cmd.Parameters.AddWithValue("@articleid", ID);
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


        //public DataTable LoadArticleComments()
        //{
        //    string sql = "SELECT * FROM comment WHERE article_id = @AID";

        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@AID", ID);
        //        MySqlDataAdapter da = new MySqlDataAdapter();

        //        da.SelectCommand = cmd;

        //        DataTable dt = new DataTable();

        //        da.Fill(dt);
        //        return dt;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
    }
}