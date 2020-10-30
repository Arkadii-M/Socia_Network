using BuisnesLogic.User;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialNetwork.Models
{
    public class AppUser : IUser
    {
        public int User_Id { get; set; }
        public AppUser(int id)
        {
            this.User_Id = id;
        }
        public AppUser()
        {
            this.User_Id = -1;
        }
    }
}