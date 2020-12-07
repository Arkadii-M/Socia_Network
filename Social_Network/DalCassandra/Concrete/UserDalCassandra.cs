using Cassandra;
using Cassandra.Mapping;
using DalCassandra.Interface;
using DalCassandra.Profiles;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Concrete
{
    public class UserDalCassandra: IUserDalCassandra
    {
        private readonly Cluster _cluster;
        private readonly string _keyspace;


        public UserDalCassandra(string keyspace= "social_network", string[] nodes=null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            _keyspace = keyspace;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            //MappingConfiguration.Global.Define<UserProfile>();
            if(MappingConfiguration.Global.Get<UserProfile>() == null)
            {
                //MappingConfiguration.Global.Define<UserProfile>();
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                return mapper.Fetch<UserDTO>().ToList();
            }
        }

        public UserDTO GetUserByLogin(string login)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                return mapper.SingleOrDefault<UserDTO>("where \"User_Login\" = ? ", login);
            }
        }

        public UserDTO InsertUser(UserDTO user)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                mapper.Insert<UserDTO>(user);

                return user;
            }
        }
    }
}
