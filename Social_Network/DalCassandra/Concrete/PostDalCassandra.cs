using DalCassandra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using DTOCassandra;
using Cassandra.Mapping;
using DTOCassandra.UDT;
using DalCassandra.Profiles;
using DalCassandra.Profiles.UDT;

namespace DalCassandra.Concrete
{
    public class PostDalCassandra: IPostDalCassandra
    {
        private readonly Cluster _cluster;
        private readonly string _keyspace;

        
        public PostDalCassandra(string keyspace = "social_network", string[] nodes =null, ConsistencyLevel consistencyLevel = ConsistencyLevel.One)
        {
            nodes = new string[] { "127.0.0.1" };
            _keyspace = keyspace;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistencyLevel))
                .Build();
            if (MappingConfiguration.Global.Get<PostProfile>() == null)
            {
                try
                {
                    MappingConfiguration.Global.Define<PostProfile>();
                }
                catch(Exception exp)
                {
                    //
                }
            }
            
        }

        public PostDTO AddCommentToPost(Guid Post_Id,Comment comment)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                mapper.Update<PostDTO>("SET \"Comments\" = \"Comments\"+ ? ,\"Modify_Date\" = ? where \"Post_Id\" = ? ", new List<Comment>() { comment }, DateTimeOffset.Now,Post_Id);
                return mapper.SingleOrDefault<PostDTO>("where \"Post_Id\" = ? ", Post_Id);
            }
        }

        public PostDTO AddCommentToPost(PostDTO post, Comment comment)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                mapper.Update<PostDTO>("SET \"Comments\" = ? + \"Comments\",\"Modify_Date\" = ? where \"Post_Id\" = ? ",new List<Comment>() { comment }, DateTimeOffset.Now, post.Post_Id);
                return mapper.SingleOrDefault<PostDTO>("where \"Post_Id\" = ? ", post.Post_Id);
            }
        }

        public PostDTO AddPost(PostDTO post)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);

                session.UserDefinedTypes.Define(new CommentProfile().Definitions);

                var TimeUUId = TimeUuid.NewId(DateTime.Now);
                post.Post_Id = TimeUUId;
                mapper.Insert(post);

                return mapper.Single<PostDTO>("where \"Post_Id\" = ? ", TimeUUId);
            }
        }

        public void DislikePost(Guid Post_Id, long User_Id)
        {
            var post = this.GetPostById(Post_Id);
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var batch = new BatchStatement();
                
                if (post.Likes.Contains(User_Id))
                {
                    batch.Add(new SimpleStatement("update posts set \"Likes\" = \"Likes\" - ? where \"Post_Id\" = ? ;", new List<long>() { User_Id }, Post_Id));
                }
                if (!post.Dislikes.Contains(User_Id))
                {
                    batch.Add(new SimpleStatement("update posts set \"Dislikes\" = \"Dislikes\" + ? where \"Post_Id\" = ? ;", new List<long>() { User_Id }, Post_Id));
                }
                session.Execute(batch);
            }
        }

        public List<PostDTO> GetAllPosts()
        {
            using(ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var posts = mapper.Fetch<PostDTO>("select * from posts");
                return posts.ToList();
            }
        }

        public PostDTO GetPostById(TimeUuid id)
        {
            using(ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var post = mapper.FirstOrDefault<PostDTO>("where \"Post_Id\" = ? ", id);
                return post;
            }
        }
        public PostDTO GetPostById(string id)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var post = mapper.FirstOrDefault<PostDTO>("where \"Post_Id\" = ? ", TimeUuid.Parse(id));
                return post;
            }
        }
        public PostDTO GetPostById(Guid id)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var post = mapper.FirstOrDefault<PostDTO>("where \"Post_Id\" = ? ", TimeUuid.Parse(id.ToString()));
                return post;
            }
        }

        public void LikePost(Guid Post_Id, long User_Id)
        {
            var post = this.GetPostById(Post_Id);
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                var batch = new BatchStatement();

                if (post.Dislikes.Contains(User_Id))
                {
                    batch.Add(  session.Prepare( "UPDATE posts SET \"Dislikes\" = \"Dislikes\" - ? WHERE \"Post_Id\" = ?").Bind(new List<long>() { User_Id }, Post_Id));

                }
                if (!post.Likes.Contains(User_Id))
                {
                    batch.Add(session.Prepare("UPDATE posts SET \"Likes\" = \"Likes\" + ? WHERE \"Post_Id\" = ?").Bind(new List<long>() { User_Id },Post_Id));
                }
                session.Execute(batch);
               // session.Execute(batch);
            }
        }

        public PostDTO UpdatePost(PostDTO post)
        {
            using (ISession session = _cluster.Connect(_keyspace))
            {
                IMapper mapper = new Mapper(session);
                session.UserDefinedTypes.Define(new CommentProfile().Definitions);
                post.Modify_Date = DateTimeOffset.Now;
                mapper.Update<PostDTO>(post);

                return mapper.SingleOrDefault<PostDTO>("where \"Post_Id\" = ?",post.Post_Id);
            }
        }
    }
}
