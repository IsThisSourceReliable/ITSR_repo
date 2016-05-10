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
        public void CreateArticle(Articles a)
        {
            try
            {
                conn.Open();

                MySqlCommand cmdCreateArticle = new MySqlCommand("INSERT INTO article (title, text, url, orgtype_id, lastedit_date, createuser_id, publisher, domainowner, financing, reference_xml) " +
                                                                "VALUES(@title, @text, @url, @typeoforg, @lasteditdate, @createuserid, @publisher, @domainowner, @financer, @referencexml);", conn);

                cmdCreateArticle.Parameters.AddWithValue("@title", a.Title);
                cmdCreateArticle.Parameters.AddWithValue("@text", a.Text);
                cmdCreateArticle.Parameters.AddWithValue("@url", a.AricleURL);
                cmdCreateArticle.Parameters.AddWithValue("@typeoforg", a.TypeOfOrg_id);
                cmdCreateArticle.Parameters.AddWithValue("@lasteditdate", a.lastEdit);
                cmdCreateArticle.Parameters.AddWithValue("@createuserid", a.createUser_id);
                cmdCreateArticle.Parameters.AddWithValue("@publisher", a.Publisher);
                cmdCreateArticle.Parameters.AddWithValue("@domainowner", a.domainOwner);
                cmdCreateArticle.Parameters.AddWithValue("@financer", a.Financing);
                cmdCreateArticle.Parameters.AddWithValue("@referencexml", a.Reference);

                cmdCreateArticle.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public void EditArticle(Articles a)
        {
            string sql = "Update article SET title = @TI, text = @TE, orgtype_id = @OT, lastedit_date = @LED, votes_up = @VU, votes_down = @VD, lastedituser_id = @LEU, createuser_id = @CU, publisher = @P, domainowner = @DO, financing = @F WHERE idarticle = @ID";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@TI", a.Title);
                cmd.Parameters.AddWithValue("@TE", a.Text);
                cmd.Parameters.AddWithValue("@OT", a.TypeOfOrg_id);
                cmd.Parameters.AddWithValue("@LED", a.lastEdit);
                cmd.Parameters.AddWithValue("@VU", a.upVotes);
                cmd.Parameters.AddWithValue("@VD", a.downVotes);
                cmd.Parameters.AddWithValue("@LEU", a.lastEditUser_id);
                cmd.Parameters.AddWithValue("@CU", a.createUser_id);
                cmd.Parameters.AddWithValue("@P", a.Publisher);
                cmd.Parameters.AddWithValue("@DO", a.domainOwner);
                cmd.Parameters.AddWithValue("@F", a.Financing);

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
            string sql2 = "UPDATE article SET votes_up = votes_up + 1 WHERE idarticle = @AID";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);

                cmd.Parameters.AddWithValue("@UID", v.user_id);
                cmd.Parameters.AddWithValue("@AID", v.article_id);
                cmd.Parameters.AddWithValue("@V", true);

                cmd2.Parameters.AddWithValue("@AID", v.article_id);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                
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
            string sql2 = "UPDATE article SET votes_down = votes_down + 1 WHERE idarticle = @AID";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);

                cmd.Parameters.AddWithValue("@UID", v.user_id);
                cmd.Parameters.AddWithValue("@AID", v.article_id);
                cmd.Parameters.AddWithValue("@V", false);

                cmd2.Parameters.AddWithValue("@AID", v.article_id);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

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
        public void ReportComment(Report r)
        {
            string sql = "INSERT INTO report_comment (comment_id, text, user_id) VALUES(@CID, @T, @UID)";

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@CID", r.articleORcomment_id);
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
        public DataTable GetArticlesCreatedBy(User u)
        {
            string sql = "SELECT * FROM article WHERE createuser_id = @userID";

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
            string sql = "SELECT * FROM comment INNER JOIN article on (comment.article_id = article.idarticle) WHERE comment.user_id = @uID";

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
            string sql = "SELECT * FROM vote INNER JOIN article on (vote.article_id = article.idarticle) WHERE vote.user_id = @uID";

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