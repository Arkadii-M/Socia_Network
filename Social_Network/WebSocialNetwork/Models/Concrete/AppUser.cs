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
        public bool Login(string name,string pass)
        {
            var u = this.Get();
            if(u.LoginAsUser(name, pass))
            {
                this.User_Id =u.GetMyId();
                return true;
            }
            return false;
        }
        public bool Register(RegisterModel user)
        {
            var u = this.Get();
            if(u.CreateNewUser(user.User_Login,user.User_Password,user.User_Name,user.User_Last_Name,user.Email,user.Interests))
            {
                this.User_Id = u.GetMyId();
                return true;
            }
            return false;
        }
        public User Get()
        {
            return new BuisnesLogic.User.User(this.User_Id);
        }

        public void Set(int id)
        {
            this.User_Id = id;
        }
    }
}