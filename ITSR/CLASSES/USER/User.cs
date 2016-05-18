using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

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

        public string firstname { get; set; }
        public string lastName { get; set; }
        public string country { get; set; }
        public string location { get; set; }
        public string occupation { get; set; }
        public int profilepic_id { get; set; }
        public string aboutme { get; set; }

        //Methods

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

        /// <summary>
        /// Overloaded method
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CheckUserNameExists()
        {
            string sql = "select exists(select 1 from user WHERE username = @UN)";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UN", userName);
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
        public bool TryLogin()
        {
            Password TryPass = new Password();
            ID = GetUserID();
            TryPass.user_id = ID;
            TryPass.PasswordInput = Password;
            bool ok = false;
            if (TryPass.TryPassword())
            {
                ok = true;
                return ok;
        }
            else
        {
                return ok;
            }
        }

        private int GetUserID()
        {
            int userID = -1;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT iduser FROM user WHERE username = @username;", conn);

                cmd.Parameters.AddWithValue("@username", userName);

                userID = Convert.ToInt32(cmd.ExecuteScalar());

                return userID;

            }
            catch (MySqlException ex)
            {
                return userID;
            }
            finally
            {
                conn.Close();
            }
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
        public void CreateUser(User user)
        {
            string sql = "INSERT INTO user (username, password, email, role_id, certified_user) VALUES(@UN, @PW, @EM, @R, @CU)";
            string sql2 = "SELECT LAST_INSERT_ID() FROM user";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);

                cmd.Parameters.AddWithValue("@UN", user.userName);
                cmd.Parameters.AddWithValue("@PW", user.Password);
                cmd.Parameters.AddWithValue("@EM", user.Email);
                cmd.Parameters.AddWithValue("@R", user.role_id);
                cmd.Parameters.AddWithValue("@CU", user.certifedUser);

                cmd.ExecuteNonQuery();
                int iduser = Convert.ToInt32(cmd2.ExecuteScalar());
                user.ID = iduser;
                conn.Close();
                CreateUserProfile(user);
            }
            catch (MySqlException ex)
            {
                string hej = ex.ToString();
            }
            finally
            {
                conn.Close();
            }

        }
        public void CreateUserProfile(User user)
        {
            string sql = "INSERT INTO user_profile (user_id, name, lastname, country, location, occupation, profilepic_id, aboutme) VALUES(@UID, @N, @LN, @C, @L, @O, @PPID, @A)";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UID", user.ID);
                cmd.Parameters.AddWithValue("@N", user.firstname);
                cmd.Parameters.AddWithValue("@LN", user.lastName);
                cmd.Parameters.AddWithValue("@C", user.country);
                cmd.Parameters.AddWithValue("@L", user.location);
                cmd.Parameters.AddWithValue("@O", user.occupation);
                cmd.Parameters.AddWithValue("@PPID", user.profilepic_id);
                cmd.Parameters.AddWithValue("@A", user.aboutme);

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
        public void LoadUser(User user)
        {
            string sql = "SELECT * FROM user WHERE iduser = @id";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", user.ID);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    user.userName = Convert.ToString(dr["username"]);
                    user.Email = Convert.ToString(dr["email"]);
                    user.role_id = Convert.ToInt16(dr["role_id"]);
                    user.certifedUser = Convert.ToBoolean(dr["certified_user"]);
                    user.Password = Convert.ToString(dr["password"]);
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
        } 
        public void LoadUserProfile(User user)
        {
            string sql = "SELECT * FROM user_profile WHERE user_id = @id";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", user.ID);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    user.firstname = Convert.ToString(dr["name"]);
                    user.lastName = Convert.ToString(dr["lastname"]);
                    user.country = Convert.ToString(dr["country"]);
                    user.location = Convert.ToString(dr["location"]);
                    user.occupation = Convert.ToString(dr["occupation"]);
                    user.profilepic_id = Convert.ToInt16(dr["profilepic_id"]);
                    user.aboutme = Convert.ToString(dr["aboutme"]);
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateUser(User user)
        {
            string sql = "Update user SET username = @UN, email = @EM WHERE iduser = @ID";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UN", user.userName);
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
        public void UpdateUserProfile(User user)
        {
            string sql = "Update user_profile SET name = @N, lastname = @LN, country = @C, location = @L, occupation = @O, profilepic_id = @PPID, aboutme = @A  WHERE user_id = @UID";
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UID", user.ID);
                cmd.Parameters.AddWithValue("@N", user.firstname);
                cmd.Parameters.AddWithValue("@LN", user.lastName);
                cmd.Parameters.AddWithValue("@C", user.country);
                cmd.Parameters.AddWithValue("@L", user.location);
                cmd.Parameters.AddWithValue("@O", user.occupation);
                cmd.Parameters.AddWithValue("@PPID", user.profilepic_id);
                cmd.Parameters.AddWithValue("@A", user.aboutme);

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