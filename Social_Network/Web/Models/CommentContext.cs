using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CommentContext:DbContext
    {
        public DbSet<DTO.CommentsDTO> Comments { get; set; }
        public DbSet<DTO.LikesDTO> Likes { get; set; }
        public DbSet<DTO.DislikeDTO> Disleks { get; set; }
    }
}