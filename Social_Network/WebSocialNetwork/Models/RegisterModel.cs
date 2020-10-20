using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialNetwork.Models
{
    public class RegisterModel
    {
        public string User_Login { get; set; }
        public string User_Password { get; set; }
        public string User_Name { get; set; }
        public string User_Last_Name { get; set; }
        public string Email { get; set; }
        public List<string> Interests { get; set; }
    }
}