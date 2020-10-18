using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialNetwork.Models
{
    public class UserModel
    {
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Last_Name { get; set; }
        public List<string> Interests { get; set; }

    }
}