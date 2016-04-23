using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Data;
using System.Text;

namespace ITSR
{
    public class Password
    {
        public int user_id { get; set; }
        public string PasswordInput { get; set; }
        public string PasswordFromDB { get; set; }
        

        //Methods

        /// <summary>
        /// Method uses input from user to create a securepassword and uses saltandhash to create and returns
        /// said new password.
        /// </summary>
        /// <returns></returns>
        public string CreateSecurePassword()
        {
            string securePassword = SaltAndHashPassword();
            return securePassword;
        }

        /// <summary>
        /// Method calls other methods to compare password input
        /// with whats in the database.
        /// </summary>
        /// <returns></returns>
        public bool TryPassword()
        {
            if (CompareSaltHash() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a randomsalt to be hashed togheter with 
        /// passordinput.
        /// </summary>
        /// <returns></returns>
        public byte[] CreateSalt()
        {
            byte[] saltBytes;
            int minSaltSize = 4;
            int maxSaltSize = 8;

            Random randomSalt = new Random();
            int saltSize = randomSalt.Next(minSaltSize, maxSaltSize);
            saltBytes = new byte[saltSize];

            //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider(); //test purpose

            //rng.GetNonZeroBytes(saltBytes); // test purpose

            return saltBytes;
        }

        /// <summary>
        /// Method salts and hashes password input.
        /// </summary>
        /// <returns></returns>
        public string SaltAndHashPassword()
        {
            //SALT
            byte[] saltBytes = CreateSalt();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);
           
            byte[] PasswordInputBytes = Encoding.UTF8.GetBytes(PasswordInput); //Passinput converted to bytearray. 
            byte[] inputAndSaltBytes = new byte[PasswordInputBytes.Length + saltBytes.Length]; //Byte array to store both passinput + salt. 

            //Adds input to inputandsaltbytes array. 
            for (int i = 0; i < PasswordInputBytes.Length; i++)
            {
                inputAndSaltBytes[i] = PasswordInputBytes[i];
            }
            //Input + salt.
            for (int i = 0; i < saltBytes.Length; i++)
            {
                inputAndSaltBytes[PasswordInputBytes.Length + i] = saltBytes[i];
            }
            //HASH
            SHA256Managed pass256 = new SHA256Managed();
            byte[] hashed = pass256.ComputeHash(inputAndSaltBytes);

            string salt = Convert.ToBase64String(saltBytes);
            string hash = Convert.ToBase64String(hashed);

            return salt + ':' + hash;
        }


        /// <summary>
        /// Method separets salt from hash thats been stored in the db.
        /// </summary>
        public void SplitSaltFromHash(out string saltOrg, out string hashOrg)
        {
            string[] splitSalt = PasswordFromDB.Split(':');

            saltOrg = Convert.ToString(splitSalt[0]);
            hashOrg = Convert.ToString(splitSalt[1]);
        }


        public bool CompareSaltHash()
        {
            string saltOrg;
            string hashOrg;

            SplitSaltFromHash(out saltOrg, out hashOrg);

            byte[] salt = Convert.FromBase64String(saltOrg);
            byte[] passBytes = Encoding.UTF8.GetBytes(PasswordInput);
            byte[] passAndSaltBytes = new byte[passBytes.Length + salt.Length];
            ////Adds input to 
            for (int i = 0; i < passBytes.Length; i++)
            {
                passAndSaltBytes[i] = passBytes[i];
            }
            //Input + salt.
            for (int i = 0; i < salt.Length; i++)
            {
                passAndSaltBytes[passBytes.Length + i] = salt[i];
            }
            //HASH
            SHA256Managed pass256 = new SHA256Managed();
            byte[] hashed = pass256.ComputeHash(passAndSaltBytes);

            string salted = Convert.ToBase64String(salt);
            string newHash = Convert.ToBase64String(hashed);

            if ((newHash == hashOrg) && (saltOrg == salted))
            {
                return true;
            }
            else
            {
                return false;
            }

            //return saltOrg + " " + hashOrg + " \n" + salted + " " + newHash; //Test purpose.
        }

        public void GetPasswordFromDb()
        {

        }
    }
}