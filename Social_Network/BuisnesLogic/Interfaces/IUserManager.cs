using DTO;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Interfaces
{
    public interface IUserManager
    {
        int GetPathLenBetweenUsers(int u1_id,int u2_id);
        List<UserLableDTO> GetPathBetweenUsers(int u1_id, int u2_id);
        List<UsersDTO> GetAllUsers();
        UsersDTO GetUserById(int id);
        UsersDTO GetUserByLogin(string login);

    }
}
