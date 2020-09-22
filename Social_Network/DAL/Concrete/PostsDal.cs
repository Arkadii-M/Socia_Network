using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DTO;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DAL.Concrete
{
    public class PostsDal : IPostsDal
    {
        private string connectionString;
        public PostsDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddCommentToPost(int id, CommentsDTO comment)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            var UpdateFilter = Builders<PostsDTO>.Update.AddToSet("Comments", comment);
            posts.UpdateOne(g => g.Post_Id == id, UpdateFilter);
        }

        public PostsDTO CreatePost(PostsDTO post)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            var count_id = posts.CountDocuments(p => p.Post_Id >= 0);
            posts.InsertOne(post);
            if (posts.CountDocuments(p => p.Post_Id >= 0) > count_id)
            {
                post.Post_Id = (int)count_id;
            }
            else
            {
                post.Post_Id = -1;
            }
            return post;
        }

        public void DeletePost(int id)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            posts.DeleteOne(p => p.Post_Id == id);
        }

        public void Dislike(int id, DislikeDTO like)  // UPDATE
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            var UpdateFilter = Builders<PostsDTO>.Update.Inc("Likes", -1);
            posts.UpdateOne(g => g.Post_Id == id, UpdateFilter);
        }

        public List<PostsDTO> GetAllPosts()
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");

            var all_posts = posts.Find(new BsonDocument()).ToList();
            return all_posts;
        }

        public PostsDTO GetPostById(int id)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            var founded = posts.Find(p => p.Post_Id == id).Single();
            return founded;
        }

        public void Like(int id, LikesDTO like) // UPDATE
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            var UpdateFilter = Builders<PostsDTO>.Update.Inc("Likes",1);
            posts.UpdateOne(g => g.Post_Id == id, UpdateFilter);
        }

        public PostsDTO UpdatePost(PostsDTO post)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("Social_Network");
            var posts = db.GetCollection<PostsDTO>("Posts");
            
            var UpdateFilter = Builders<PostsDTO>.Update.Set("Post_Id", post.Post_Id); // ?
            var UpdateDef = UpdateFilter;

            if (post.Post_Id != 0) { UpdateDef.Set("Post_Id", post.Post_Id); }
            if (post.Author_Id != 0) { UpdateDef.Set("Author_Id", post.Author_Id); }
            if (post.Title != null) { UpdateDef.Set("Title", post.Title); }
            if (post.Body != null) { UpdateDef.Set("Body", post.Body); }
            if (post.Tags != null) { UpdateDef.Set("Tags", post.Tags); }

            UpdateDef.Set("Modify", DateTime.Now);


            posts.UpdateOne(g => g.Post_Id == post.Post_Id, UpdateDef);
            var res = posts.Find(p => p.Post_Id == post.Post_Id).Single();
            return res;
            
        }

    }
}
