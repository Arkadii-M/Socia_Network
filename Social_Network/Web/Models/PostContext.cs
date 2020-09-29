using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DTO;

namespace Web.Models
{
    public class PostContext:DbContext
    {
        public DbSet<DTO.PostsDTO> Posts { get; set; }
        public DbSet<DTO.CommentsDTO> Comments { get; set; }
        public DbSet<DTO.LikesDTO> Likes { get; set; }
        public DbSet<DTO.DislikeDTO> Disleks { get; set; }

    }
}