using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.USER
{
    public class Administrator:Moderator
    {

        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        //Methods
        public void UpdateMemberToModerator(Member m)
        {
            string sql = "Update user SET role_id = @RID WHERE iduser = @ID";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RID", 2);
                cmd.Parameters.AddWithValue("@ID", m.ID);

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
        public void UpdateModeratorToAdministrator(Moderator m)
        {
            string sql = "Update user SET role_id = @RID WHERE iduser = @ID";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RID", 3);
                cmd.Parameters.AddWithValue("@ID", m.ID);

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
        public void MakeMemberCertified(Member m)
        {
            string sql = "Update user SET certfied_user = @CEU WHERE iduser = @ID";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CEU", true);
                cmd.Parameters.AddWithValue("@ID", m.ID);

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
        public void MakeCertifiedMemberNotCertified(Member m)
        {
            string sql = "Update user SET certfied_user = @CEU WHERE iduser = @ID";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CEU", false);
                cmd.Parameters.AddWithValue("@ID", m.ID);

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