using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialNetwork.Models
{
    public class UsersPathModel
    {
        public int UserFromId { get; set; }
        public int UserToId { get; set; }
        public List<UserModel> PathToUser { get; set; }
        public int PathLen { get { return this.PathToUser.Count; } }
    }
}