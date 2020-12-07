using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
namespace WebSocialNetwork.Models
{
    public class PostModel
    {
        public Guid Post_Id { get; set; }
        public string Author_FullName { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public List<string> Tags { get; set; }

        public List<long> Likes { get; set; }

        public List<long> Dislikes { get; set; }

        public List<DTOCassandra.UDT.Comment> Comments { get; set; }

    }
}