using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalNeo4j.Concrete;
using DTONeo4j;

using BuisnesLogic.User;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            BuisnesLogic.User.User _user = new BuisnesLogic.User.User(0);
            _user.CreateNewUser("Tesxadvabhd", "", "", "", "", new List<string>());
            var all = _user.GetAllUsers();

            /*
            // var db = new DalNeo4j.Concrete.
            var connectionString = "http://localhost:7474/db/data/";
            var login = "neo4j";
            var pass = "1234567890";
            var db = new  DalNeo4j.Concrete.UsersDalNeo4j(connectionString,login,pass);
            //db.AddUser(new UserLableDTO() { User_Id = 4, User_Login = "try", User_Name = "A", User_Last_Name = "B" });
            //db.AddRelationship(new UserLableDTO() { User_Id = 1 }, new UserLableDTO() { User_Id = 4 });
            //db.DeleteRelationship(new UserLableDTO() { User_Id = 1 }, new UserLableDTO() { User_Id = 4 });
            //db.DeleteUser(new UserLableDTO() { User_Id = 4 });
            db.MinPathBetweenList(1, 3);
            */
        }
    }
}
