using BuisnesLogic.Interfaces;
using DTO;
using DalNeo4j;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete;
using DAL.Interfaces;
using DalNeo4j.Interfaces;
using DalNeo4j.Concrete;

namespace BuisnesLogic.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly string mongo_connectionString = "mongodb://localhost:27017/";
        private readonly string neo4j_connectionString = "http://localhost:7474/db/data/";
        private readonly string neo4j_login = "neo4j";
        private readonly string neo4j_pass = "1234567890";

        private readonly IUsersDal _mongoUserDal;
        private readonly IUsersDalNeo4j _neo4jUserDal;
        public UserManager()
        {
            this._mongoUserDal = new UsersDal(this.mongo_connectionString);
            this._neo4jUserDal = new UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
        }
        public UserManager(IUsersDal mongoUserDal, IUsersDalNeo4j neo4jUserDal)
        {
            this._mongoUserDal = mongoUserDal;
            this._neo4jUserDal = neo4jUserDal;
        }


        public UserManager(string mongo_conn)
        {
            this.mongo_connectionString = mongo_conn;
            this._mongoUserDal = new UsersDal(this.mongo_connectionString);
            this._neo4jUserDal = new UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
        }
        public UserManager(string neo4j_conn,string login,string pass)
        {

            this.neo4j_connectionString = neo4j_conn;
            this.neo4j_login = login;
            this.neo4j_pass = pass;
            this._mongoUserDal = new UsersDal(this.mongo_connectionString);
            this._neo4jUserDal = new UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
        }
        public UserManager(string mongo_conn, string neo4j_conn, string login, string pass)
        {
            this.mongo_connectionString = mongo_conn;
            this.neo4j_connectionString = neo4j_conn;
            this.neo4j_login = login;
            this.neo4j_pass = pass;
            this._mongoUserDal = new UsersDal(this.mongo_connectionString);
            this._neo4jUserDal = new UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
        }
        public List<UsersDTO> GetAllUsers()
        {
            return this._mongoUserDal.GetAllUsers();
        }

        public List<UserLableDTO> GetPathBetweenUsers(int u1_id, int u2_id)
        {
            var path = this._neo4jUserDal.MinPathBetweenList(u1_id, u2_id);
            return path;
        }

        public int GetPathLenBetweenUsers(int u1_id, int u2_id)
        {
            var path = this._neo4jUserDal.MinPathBetweenList(u1_id, u2_id);
            return path.Count;
        }

        public UsersDTO GetUserById(int id)
        {
            return this._mongoUserDal.GetUserById(id);
        }

        public UsersDTO GetUserByLogin(string login)
        {
            return this._mongoUserDal.GetUserByLogin(login);
        }

        public void CreateRelationship(int id_from, int id_to)
        {
            DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(this.mongo_connectionString);
            var u = dal.GetUserById(id_from);
            if(u.Friends_Ids.Contains(id_to))
            {
                return;
            }
            else
            {
                u.Friends_Ids.Add(id_to);
                u = dal.UpdateUser(u);
                DalNeo4j.Concrete.UsersDalNeo4j Neo4jDal = new DalNeo4j.Concrete.UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
                Neo4jDal.AddRelationship(id_from, id_to);
            }

        }

        public bool CreateUser(UsersDTO u)
        {
            try
            {
                this._mongoUserDal.CreateUser(u);
                this._neo4jUserDal.AddUser(new UserLableDTO { User_Id = u.User_Id, User_Name = u.User_Name, User_Last_Name = u.User_Last_Name, User_Login = u.User_Login });
            }
            catch(Exception exp)
            {
                return false;
                // add reaction
            }
            return true;
        }
    }
}
