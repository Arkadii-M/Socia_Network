using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using DalCassandra;
using DalCassandra.Concrete;
using DTOCassandra;
using DTOCassandra.UDT;
using DalNeo4j.Concrete;


namespace ConsoleForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersDalNeo4j dal = new UsersDalNeo4j("http://localhost:7474/db/data/", "neo4j", "1234567890");
            dal.GetUser(2);
            dal.GetAllFriendsIdForUser(2);
            //var nodes = new string[] { "127.0.0.1" };
           // var cDal = new PostDalCassandra(keyspace: "social_network", nodes: nodes);
            //cDal.DislikePost(Guid.Parse("a3e9e670-3248-11eb-8d40-616fb3fa4af3"), 1);
            /*
            var all = cDal.GetAllPosts();
            all.First();
            var added_post = cDal.AddPost(new PostDTO { 
                Author_Id=1,
                Body= "New Post",
                Title ="Title",
                Create_Date= DateTime.Now,
                Modify_Date = DateTime.Now,
                Dislikes=new List<long>() { 1 },
                Likes=new List<long>() { 2 },
                Comments = new List<Comment>() { new Comment() { Body="NEW COMMENT",User_Id=1,Create_Date=DateTimeOffset.Now,Modify_Date= DateTimeOffset.Now } }

            });

            all = cDal.GetAllPosts();
            all.First();
            
            var to_update = cDal.GetAllPosts()[1];
            Console.WriteLine("Before updated: {0}", to_update.Body);
            to_update.Body = "Changed body to check Update method";
            var updated =cDal.UpdatePost(to_update);
            Console.WriteLine("Updated: {0}",updated.Body);
            Console.ReadLine();
            */
            /*
            var added_post = cDal.AddPost(new PostDTO
            {
                Author_Id = 1,
                Body = "New Post",
                Title = "Title",
                Create_Date = DateTime.Now,
                Modify_Date = DateTime.Now,
                Dislikes = new List<long>() { 1 },
                Likes = new List<long>() { 2 },
                Comments = new List<Comment>() { new Comment() { Body = "NEW COMMENT", User_Id = 1, Create_Date = DateTimeOffset.Now, Modify_Date = DateTimeOffset.Now } }

            });

            var posts = cDal.GetAllPosts();
            var streamDal = new UserStreamDalCassandra(keyspace: "social_network", nodes: nodes);
            
            foreach(var p in posts)
            {
                streamDal.AddPostToUsersStreams(p, new List<long>() { 1 });
            }
            */
            //streamDal.GetStreamForUser(1);
            //streamDal.AddPostToUsersStreams(posts[0], new List<string>() {"Login"});
            
            //var uDal = new UserDalCassandra(keyspace: "social_network", nodes: nodes);
            //uDal.GetAllUsers();

        }
    }
}
