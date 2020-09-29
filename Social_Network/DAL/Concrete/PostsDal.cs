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
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var UpdateFilter = Builders<PostsDTO>.Update.AddToSet("Comments", comment);
                posts.UpdateOne(g => g.Post_Id == id, UpdateFilter);
            }
            catch(Exception exp)
            {
                throw exp;
            }

            
        }
        public void DeleteComment(int post_id, int comment_id) // not checked
        {
            try
            {
                var post = this.GetPostById(post_id);
                var comment = post.Comments[comment_id - 1];
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                
                var UpdateFilter = Builders<PostsDTO>.Update.Pull("Comments", comment);
                posts.UpdateOne(g => g.Post_Id == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            throw new NotImplementedException();
        }

        public PostsDTO CreatePost(PostsDTO post)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var count_id = posts.CountDocuments(p => p.Post_Id > 0);
                post.Post_Id = (int)count_id + 1;
                posts.InsertOne(post);
                return post;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void DeletePost(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                posts.DeleteOne(p => p.Post_Id == id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void Dislike(int id, DislikeDTO dislike)  //
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var UpdateFilter = Builders<PostsDTO>.Update.AddToSet("Dislikes",dislike);
                posts.UpdateOne(g => g.Post_Id == id, UpdateFilter);
             }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public void UnDislike(int post_id, DislikeDTO dislike)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var UpdateFilter = Builders<PostsDTO>.Update.Pull("Dislikes", dislike);
                posts.UpdateOne(g => g.Post_Id == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public void Like(int id, LikesDTO like) // 
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var UpdateFilter = Builders<PostsDTO>.Update.AddToSet("Likes", like);
                posts.UpdateOne(g => g.Post_Id == id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public void UnLike(int post_id, LikesDTO like)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var UpdateFilter = Builders<PostsDTO>.Update.Pull("Likes", like);
                posts.UpdateOne(g => g.Post_Id == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<PostsDTO> GetAllPosts()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");

                var all_posts = posts.Find(p => p.Post_Id > 0).ToList();
                return all_posts;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public PostsDTO GetPostById(int id)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
                var founded = posts.Find(p => p.Post_Id == id).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

       
        public PostsDTO UpdatePost(PostsDTO post)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("Social_Network");
                var posts = db.GetCollection<PostsDTO>("Posts");
            
                var UpdateFilter = Builders<PostsDTO>.Update.Set("Post_Id", post.Post_Id); // ?

                if (post.Author_Id != 0) { UpdateFilter = UpdateFilter.Set("Author_Id", post.Author_Id); }
                if (post.Title != null) { UpdateFilter = UpdateFilter.Set("Title", post.Title); }
                if (post.Body != null) { UpdateFilter=UpdateFilter.Set("Body", post.Body); }
                if (post.Tags != null) { UpdateFilter=UpdateFilter.Set("Tags", post.Tags); }

                UpdateFilter=UpdateFilter.Set("Modify", DateTime.Now);


                posts.UpdateOne(g => g.Post_Id == post.Post_Id, UpdateFilter);
                var res = posts.Find(p => p.Post_Id == post.Post_Id).Single();
            return res;
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }

        
    }
}
