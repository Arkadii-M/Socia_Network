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

        private readonly IUsersDal _mongoUserDal;
        private readonly IUsersDalNeo4j _neo4jUserDal;

        public UserManager(IUsersDal mongoUserDal, IUsersDalNeo4j neo4jUserDal)
        {
            this._mongoUserDal = mongoUserDal;
            this._neo4jUserDal = neo4jUserDal;
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

            var u = this._mongoUserDal.GetUserById(id_from);
            if(u.Friends_Ids.Contains(id_to))
            {
                return;
            }
            else
            {
                u.Friends_Ids.Add(id_to);
                u = this._mongoUserDal.UpdateUser(u);
                this._neo4jUserDal.AddRelationship(id_from, id_to);
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
