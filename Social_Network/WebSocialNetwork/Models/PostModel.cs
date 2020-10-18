using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
namespace WebSocialNetwork.Models
{
    public class PostModel
    {
        public int Post_Id { get; set; }
        public string Author_FullName { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public List<string> Tags { get; set; }

        public List<LikesDTO> Likes { get; set; }

        public List<DislikeDTO> Dislikes { get; set; }

        public List<CommentsDTO> Comments { get; set; }

    }
}