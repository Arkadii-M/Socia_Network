using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalNeo4j.Interfaces
{
    public interface IUsersDalNeo4j
    {
        void AddUser(UserLableDTO u);
        void DeleteUser(UserLableDTO u); // neet to delete all relationships
        UserLableDTO GetUser(int id); 
        void DeleteRelationship(UserLableDTO u1, UserLableDTO u2);
        void AddRelationship(UserLableDTO u1, UserLableDTO u2);
        bool HasRelationship(UserLableDTO u1, UserLableDTO u2);
        int MinPathBetween(UserLableDTO u1, UserLableDTO u2);
        int MinPathBetween(int id1, int id2);
        List<UserLableDTO> MinPathBetweenList(int id1, int id2);
        // get relationship


    }
}
