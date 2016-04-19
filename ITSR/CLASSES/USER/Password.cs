using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.USER
{
    public class Password
    {
        public int user_id { get; set; }
        public string passwordInput { get; set; }
        public string passwordOrg { get; set; }
        

        //Methods
        public void CreateSecurePassword()
        {
            
        }
        public void TryPassword()
        {

        }
        public void CreateSalt()
        {

        }
        public void SaltAndHashPassword()
        {

        }
        public void SplitSaltFromHash()
        {

        }
        public void CompareSaltHash()
        {

        }
        public void GetPasswordFromDb()
        {

        }
    }
}