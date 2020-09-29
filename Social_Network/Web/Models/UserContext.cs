using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DTO;

namespace Web.Models
{
    public class UserContext :DbContext
    {
        public DbSet<DTO.UsersDTO> Users { get; set; }

    }
}