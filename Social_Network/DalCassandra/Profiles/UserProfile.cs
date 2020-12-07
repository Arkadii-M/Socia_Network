using Cassandra.Mapping;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Profiles
{
    public class UserProfile : Mappings
    {
        public UserProfile()
        {
            For<UserDTO>()
                 .TableName("users")
                 .PartitionKey(u => u.User_Login)
                 .CaseSensitive()
                 .Column(u => u.User_Login, cm => cm.WithName("User_Login"))
                 .Column(u => u.User_Id, cm => cm.WithName("User_Id"));
        }
    }
}
