using BuisnesLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;
using DalNeo4j.Concrete;
using DalNeo4j.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly string mongo_connectionString = "mongodb://localhost:27017/";
        private readonly string neo4j_connectionString = "http://localhost:7474/db/data/";
        private readonly string neo4j_login = "neo4j";
        private readonly string neo4j_pass = "1234567890";
        private readonly IUsersDal _mongoUserDal;
        private readonly IUsersDalNeo4j _neo4jUserDal;
        public AuthManager()
        {
            this._mongoUserDal = new UsersDal(this.mongo_connectionString);
            this._neo4jUserDal = new UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
        }
        public AuthManager(string mongo_connectionString)
        {
            this.mongo_connectionString = mongo_connectionString;
            this._mongoUserDal = new UsersDal(this.mongo_connectionString);
            this._neo4jUserDal = new UsersDalNeo4j(this.neo4j_connectionString, this.neo4j_login, this.neo4j_pass);
        }
        public AuthManager(IUsersDal mongoUserDal, IUsersDalNeo4j neo4jUserDal)
        {
            this._mongoUserDal = mongoUserDal;
            this._neo4jUserDal = neo4jUserDal;
        }
        public bool Login(string UName, string UPass)
        {
            UsersDTO temp;
            try
            {
                temp = this._mongoUserDal.GetUserByLogin(UName);
            }
            catch(Exception exp)
            {
                return false;
            }
            if(temp != null && temp.User_Password == UPass)
            {
                    return true;
            }
            return false;
        }
    }
}
