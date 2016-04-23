using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSR.CLASSES.USER;

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

        public void CreateArticle(Articles a)
        {
            string sql = "INSERT INTO article (title, text, orgtype_id, lastedit_date, votes_up, votes_down, lastedituser_id, createuser_id, publisher, doaminowner, financing) VALUES(@TI, @TE, @OT, @LED, @VU, @VD, @LEU, @CU, @P, @DO, @F)";

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
    }
}