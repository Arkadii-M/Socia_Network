using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialNetwork.Models
{
    public class CommentModel
    {
        public long User_Id { get; set; }
        public string Body { get; set; }
    }
}