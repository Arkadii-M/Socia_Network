using DTO;
using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogic;


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
                return false;
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
        public bool CreateNewUser(string Login, string Pwd, string Name, string L_Name, string _Email, List<string> Interests)
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
            DTO.UsersDTO new_user = new DTO.UsersDTO
            {
                User_Login = Login,
                User_Password = Pwd,
                User_Name = Name,
                User_Last_Name = L_Name,
                Interests = Interests,
                Reg_Date = new BsonTimestamp(DateTime.UtcNow.ToBinary()),
                Email = _Email,
                Friends_Ids = new List<int>()
            };
            try
            {
                var res = dal.GetUserByLogin(Login);
                if (res.Id != null)
                {
                    throw new Exception("User with this login already created");
                }
            }
            catch (Exception exp)
            {
                if(exp.Message == "User with this login already created")
                {
                    return false;
                }
            }
            this.Account = dal.CreateUser(new_user);
            if (Account.Id != null)
            {
                this.Logined = true;
                return true;
                
            }

            return false;
        }
        public List<UsersDTO> GetAllUsers()
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
            return dal.GetAllUsers();
        }
        public string GetMyName()
        {
            return this.Account.User_Name;
        }
        public string GetMyLastName()
        {
            return this.Account.User_Last_Name;
        }
        public string GetMyLogin()
        {
            return this.Account.User_Login;
        }
        public string GetMyEmail()
        {
            return this.Account.Email;
        }
        public List<string> GetMyFriendsList()
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
            var list = this.Account.Friends_Ids;
            var res = new List<string>();
            foreach(var p in list)
            {
                var temp = dal.GetUserById(p);
                res.Add(temp.User_Name+ " "+temp.User_Last_Name);
            }
            return res;
        }
        public List<string> GetMyInterestsList()
        {
            return this.Account.Interests;
        }
        //Todo: delete account
    }
}
