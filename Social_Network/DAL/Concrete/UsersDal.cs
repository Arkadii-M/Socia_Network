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
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var users = db.GetCollection<UsersDTO>("Users");
            users.InsertOne(user);

            return user;
        }

        public void DeleteUser(int id)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var users = db.GetCollection<UsersDTO>("Users");

            //var filterbuilder = Builders<BsonDocument>.Filter;
            //var filter = filterbuilder.Eq("User_Id",(int)id);
            users.DeleteOne(p => p.User_Id == id);
        }

        public List<UsersDTO> GetAllUsers()
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var users = db.GetCollection<UsersDTO>("Users");

            var all_users = users.Find(new BsonDocument()).ToList();
            return all_users;

        }

        public UsersDTO GetUserById(int id)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var users = db.GetCollection<UsersDTO>("Users");


            var founded = users.Find(p => p.User_Id == id).Single();
            return founded;
        }

        public UsersDTO UpdateUser(UsersDTO user)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var users = db.GetCollection<UsersDTO>("Users");

            var UpdateFilter = Builders<UsersDTO>.Update.Set("User_Id",user.User_Id); // ?
            var UpdateDef = UpdateFilter;

            if (user.User_Login != null) { UpdateDef.AddToSet("User_Login", user.User_Login); }
            if (user.User_Password != null) { UpdateDef.AddToSet("User_Password", user.User_Password); }
            if (user.User_Name != null) { UpdateDef.AddToSet("User_Name", user.User_Name); }
            if (user.User_Last_Name != null) { UpdateDef.AddToSet("User_Last_Name", user.User_Last_Name); }
            if (user.Friends_Ids != null) { UpdateDef.AddToSet("Friends_Ids", user.Friends_Ids); }
            if (user.Interests != null) { UpdateDef.AddToSet("Interests", user.Interests); }
            
            users.UpdateOne(g => g.User_Id == user.User_Id, UpdateDef);
            var res = users.Find(p=> p.User_Id == user.User_Id).Single();
            return res;
            

        }
    }
}
