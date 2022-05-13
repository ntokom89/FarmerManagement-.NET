using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;

namespace PROG7311_Task_2.Models
{
    public class User
    {
        public string userId { get; set; }
        public string userType { get; set; }

        public string userEmail { get; set; }

        public SecureString password { get; set; }

        public User(string userId, string userType, string userEmail, SecureString password)
        {
            this.userId = userId;
            this.userType = userType;
            this.userEmail = userEmail;
            this.password = password;
        }

        public User()
        {
        }
    }
}