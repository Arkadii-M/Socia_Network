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
        
        private int user_id;
        private bool Logined;
        public User(int id)
        {
            this.user_id = id;
            Logined = false;
        }
        public bool LoginAsUser(string login,string pass)
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
                if (temp.User_Password == pass)
                {
                    this.user_id = temp.User_Id;
                    Logined = true;
                    return true;
                }
            }

            return false;
        }
        public void Unlogin()
        {
            this.user_id = 0;
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
            var Account = dal.CreateUser(new_user);
            if (Account.Id != null)
            {
                this.user_id = Account.User_Id;
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
        private UsersDTO GetMe()
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
            return dal.GetUserById(this.user_id);
        }
        public int GetMyId()
        {
            return this.user_id;
        }
        public string GetMyName()
        {
            return GetMe().User_Name;
        }
        public string GetMyLastName()
        {

            return GetMe().User_Last_Name;
        }
        public string GetMyLogin()
        {

            return GetMe().User_Login;
        }
        public string GetMyEmail()
        {
            return GetMe().Email;
        }
        public List<string> GetMyFriendsList()
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
            var list = this.GetMe().Friends_Ids;
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
            return this.GetMe().Interests;
        }
        //Todo: delete account
    }
}
