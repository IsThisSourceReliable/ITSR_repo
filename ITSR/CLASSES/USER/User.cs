using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.USER
{
    public class User
    {
        MySqlConnection conn = new MySqlConnection("Database=itsrdb; Data Source=eu-cdbr-azure-north-e.cloudapp.net; User Id=b268b5fbbce560; Password=d722d6d4");

        public int ID { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int role_id { get; set; }
        public bool certifedUser { get; set; }

        //Methods
        public void CreateUser(User user)
        {
            string sql = "INSERT INTO user (username, password, email, role_id, certified_user) VALUES(@UN, @PW, @EM, @R, @CU)";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UN", user.userName);
                cmd.Parameters.AddWithValue("@PW", user.Password);
                cmd.Parameters.AddWithValue("@EM", user.Email);
                cmd.Parameters.AddWithValue("@R", user.role_id);
                cmd.Parameters.AddWithValue("@CU", user.certifedUser);

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
        public bool CheckUserNameExists(User user)
        {
            string sql = "select exists(select 1 from user WHERE username = @UN)";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UN", user.userName);
                bool exists = Convert.ToBoolean(cmd.ExecuteScalar());

                if (exists)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                return true;
            }
            finally
            {
                conn.Close();
            }          
        }
        public void TryLogin()
        {

        }
        public int CheckUserLvl(User user)
        {
            string sql = "SELECT role_id FROM user WHERE iduser = @ID";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", user.ID);

                int userLvl = Convert.ToInt16(cmd.ExecuteScalar());

                return userLvl;

            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return 0;
        }
        public void UpdateInfo(User user)
        {
            string sql = "Update user SET username = @UM, password = @PW, email = @EM WHERE iduser = @ID";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UN", user.userName);
                cmd.Parameters.AddWithValue("@PW", user.Password);
                cmd.Parameters.AddWithValue("@EM", user.Email);
                cmd.Parameters.AddWithValue("@ID", user.ID);

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
        public bool CheckEmail(User user)
        {
            string sql = "select exists(select 1 from user WHERE email = @EM)";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@EM", user.Email);
                bool exists = Convert.ToBoolean(cmd.ExecuteScalar());

                if (exists)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                return true;
            }
            finally
            {
                conn.Close();
            }
        }
    }    
}