using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSR.CLASSES.USER
{
    public abstract class User 
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int role_id { get; set; }
        public bool certifedUser { get; set; }



        //Methods
        public void CreateUser()
        {

        }
        public void CheckUserNameExists()
        {

              
        }
        public void TryLogin()
        {

        }
        public void CheckUserLvl()
        {

        }      
        public void UpdateInfo()
        {

        }       
        public void CheckEmail()
        {

        }
    }
}