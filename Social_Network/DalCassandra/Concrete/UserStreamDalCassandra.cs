using AutoMapper;
using Cassandra;
using Cassandra.Mapping;
using DalCassandra.Interface;
using DalCassandra.Profiles;
using DalCassandra.Profiles.UDT;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Concrete
{
    public class UserStreamDalCassandra : IUserStreamDalCassandra
    {
        private readonly Cluster _cluster;
        private readonly string _keyspace;
        private AutoMapper.IMapper _StreamMapper;
        public UserStreamDalCassandra(string keyspace = "social_network", string[] nodes = null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            var conf = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserStreamProfile());
                mc.AddProfile(new PostToStreamProfile());
            });

            _StreamMapper = conf.CreateMapper();
            _keyspace = keyspace;
            _cluster= Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            if (MappingConfiguration.Global.Get<StreamProfile>() == null)
            {
                try
                {

                    MappingConfiguration.Global.Define<StreamProfile>();
                }
                catch(Exception exp)
                {

                }
            }
            if (MappingConfiguration.Global.Get<UserProfile>() == null)
            {
                try
                {
                    MappingConfiguration.Global.Define<UserProfile>();
                }
                catch(Exception exp)
                {

                }
            }
            if(MappingConfiguration.Global.Get<UserStreamProfileCassandra>() == null)
            {
                try
                {
                    MappingConfiguration.Global.Define<UserStreamProfileCassandra>();
                }
                catch(Exception exp)
                {

                }
            }
           
        }
        public void AddPostToUsersStreams(PostDTO post, List<long> UsersIds)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                StreamDTO Stream = _StreamMapper.Map<StreamDTO>(post);
                foreach(var u in UsersIds)
                {
                    Stream.User_Id = u;
                    mapper.Insert(Stream);
                }
            }
        }

        public void AddPostToUsersStreams(PostDTO post, List<string> UsersLogins)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                StreamDTO Stream = _StreamMapper.Map<StreamDTO>(post);

                var Ids = mapper.Fetch<UserDTO>("where \"User_Login\" in (?)",UsersLogins.ToArray()).ToList();
                this.AddPostToUsersStreams(post, Ids.Select(p => p.User_Id).ToList());

            }
        }

        public UserStreamDTO GetStreamForUser(long id,int LIMIT = 20)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                var user_stream =mapper.Fetch<UserStream>("where \"User_Id\" = ? LIMIT ? ",id,LIMIT);
                return _StreamMapper.Map<UserStreamDTO>(user_stream.ToList());
            }
        }

        public void UpdateStreamPost(PostDTO post,List<long> UsersIds)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                Cassandra.Mapping.IMapper mapper = new Cassandra.Mapping.Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                foreach(var user in UsersIds)
                {
                    mapper.Update<StreamDTO>("SET \"Likes\" =? ," +
                        "\"Dislikes\" =?," +
                        "\"Comments\"=?," +
                        "\"Modify_Date\" = ?" +
                        "where \"User_Id\" = ? and \"Post_Id\" = ? ",post.Likes,post.Dislikes,post.Comments,post.Modify_Date,user,post.Post_Id);
                }
            }
        }
    }
}
