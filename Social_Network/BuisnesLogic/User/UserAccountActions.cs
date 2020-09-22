using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.User
{
    public partial class User
    {
        private const string conn = "mongodb://localhost:27017/";
        private DTO.UsersDTO Account;
        private bool Logined;
        public User()
        {
            this.Account = null;
            Logined = false;
        }
        public bool LoginAsUser(string login,string pwd)
        {
            DAL.Concrete.UsersDal user = new DAL.Concrete.UsersDal(conn);
            UsersDTO temp;
            try
            {

                temp = user.GetUserByLogin(login);
            }
            catch(Exception exp)
            {
                throw exp;
            }
            if(temp.Id != null)
            {
                if(temp.User_Password == pwd)
                {
                    this.Account = temp;
                    Logined = true;
                    return true;
                }
            }

            return false;
        }
        public void Unlogin()
        {
            this.Account = null;
            this.Logined = false;
        }

        public bool CreateNewUser(string Login,string Pwd,string Name,string L_Name,string Email,string Interests)
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
            DTO.UsersDTO new_user = new DTO.UsersDTO { User_Login = Login,
                User_Password = Pwd,
                User_Name = Name,
                User_Last_Name = L_Name,
                Interests = Interests.Split().ToList(),
                Reg_Date = DateTime.UtcNow.ToString(),
                Email =Email
            };
            try
            {
                var res = dal.GetUserByLogin(Login);
                if (res.Id != null)
                {
                    throw new Exception("User with this login already created");
                }
            }
            catch(Exception exp)
            {
                throw exp;
            }
            this.Account = dal.CreateUser(new_user);
            this.Logined = true;
            if (Account.Id != null)
                return true;

            return false;
        }
        //Todo: delete account
    }
}
