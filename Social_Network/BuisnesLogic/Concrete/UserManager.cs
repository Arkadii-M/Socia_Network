using BuisnesLogic.Interfaces;
using DTO;
using DalNeo4j;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly string mongo_connectionString = "mongodb://localhost:27017/";
        private readonly string neo4j_connectionString = "http://localhost:7474/db/data/";
        private readonly string neo4j_login = "neo4j";
        private readonly string neo4j_pass = "1234567890";
        public UserManager()
        {

        }
        public UserManager(string mongo_conn)
        {
            this.mongo_connectionString = mongo_conn;
        }
        public UserManager(string neo4j_conn,string login,string pass)
        {

            this.neo4j_connectionString = neo4j_conn;
            this.neo4j_login = login;
            this.neo4j_pass = pass;
        }
        public UserManager(string mongo_conn, string neo4j_conn, string login, string pass)
        {
            this.mongo_connectionString = mongo_conn;
            this.neo4j_connectionString = neo4j_conn;
            this.neo4j_login = login;
            this.neo4j_pass = pass;
        }
        public List<UsersDTO> GetAllUsers()
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(this.mongo_connectionString);
            return dal.GetAllUsers();
        }

        public List<UserLableDTO> GetPathBetweenUsers(int u1_id, int u2_id)
        {
            DalNeo4j.Concrete.UsersDalNeo4j dal = new DalNeo4j.Concrete.UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
            var path = dal.MinPathBetweenList(u1_id, u2_id);
            return path;
        }

        public int GetPathLenBetweenUsers(int u1_id, int u2_id)
        {
            DalNeo4j.Concrete.UsersDalNeo4j dal = new DalNeo4j.Concrete.UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
            var path = dal.MinPathBetweenList(u1_id, u2_id);
            return path.Count;
        }

        public UsersDTO GetUserById(int id)
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(this.mongo_connectionString);
            return dal.GetUserById(id);
        }

        public UsersDTO GetUserByLogin(string login)
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(this.mongo_connectionString);
            return dal.GetUserByLogin(login);
        }
    }
}
