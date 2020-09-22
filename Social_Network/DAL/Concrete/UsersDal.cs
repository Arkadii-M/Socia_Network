using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UsersDal : IUsersDal
    {
        private string connectionString;

        public UsersDal(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public UsersDTO CreateUser(UsersDTO user)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var users = db.GetCollection<UsersDTO>("Users");
                var count_id = users.CountDocuments(p => p.User_Id >=0);
                user.User_Id = (int)count_id + 1;
                users.InsertOne(user);
                return user;
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        public void DeleteUser(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var users = db.GetCollection<UsersDTO>("Users");
                users.DeleteOne(p => p.User_Id == id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<UsersDTO> GetAllUsers()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var users = db.GetCollection<UsersDTO>("Users");

                var all_users = users.Find(p => p.User_Id >= 0).ToList();
                return all_users;
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        public UsersDTO GetUserById(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var users = db.GetCollection<UsersDTO>("Users");


                var founded = users.Find(p => p.User_Id == id).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public UsersDTO GetUserByLogin(string login)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var users = db.GetCollection<UsersDTO>("Users");
                var founded = users.Find(p => p.User_Login == login).Single();
                return founded;
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }

        public UsersDTO UpdateUser(UsersDTO user)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var users = db.GetCollection<UsersDTO>("Users");

                var UpdateFilter = Builders<UsersDTO>.Update.Set("User_Id", user.User_Id); // ?

                if (user.User_Login != null) { UpdateFilter = UpdateFilter.Set("User_Login", user.User_Login);}
                if (user.User_Password != null) { UpdateFilter =UpdateFilter.Set("User_Password", user.User_Password); }
                if (user.User_Name != null) { UpdateFilter=UpdateFilter.Set("User_Name", user.User_Name); }
                if (user.User_Last_Name != null) { UpdateFilter= UpdateFilter.Set("User_Last_Name", user.User_Last_Name); }
                if (user.Friends_Ids != null) { UpdateFilter=UpdateFilter.Set("Friends_Ids", user.Friends_Ids); }
                if (user.Interests != null) { UpdateFilter=UpdateFilter.Set("Interests", user.Interests); }
                if (user.Email != null) { UpdateFilter = UpdateFilter.Set("Email", user.Email); }

                users.UpdateOne(g => g.User_Id == user.User_Id, UpdateFilter);
                var res = users.Find(p=> p.User_Id == user.User_Id).Single();
                return res;
            }
            catch (Exception exp)
            {
                throw exp;
            }


        }
    }
}
