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
        private readonly IUsersDal _mongoUserDal;
        private readonly IUsersDalNeo4j _neo4jUserDal;
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
