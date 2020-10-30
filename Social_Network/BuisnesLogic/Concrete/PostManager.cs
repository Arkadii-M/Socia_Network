using BuisnesLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;
using DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Concrete
{
    public class PostManager: IPostManager
    {
        private readonly string mongo_connectionString = "mongodb://localhost:27017/";
        private readonly IPostsDal _postDal;
        public PostManager()
        {
            this._postDal = new PostsDal(this.mongo_connectionString);
        }
        public PostManager(IPostsDal userDal)
        {
            this._postDal = userDal;
        }
        public PostManager(string mongo_conn)
        {
            this.mongo_connectionString = mongo_conn;
            this._postDal =new  PostsDal(this.mongo_connectionString);
        }

        public void AddCommentToPost(int PostId, int Author_Id, string Comment_Text)
        {
           this._postDal.AddCommentToPost(PostId,
               new CommentsDTO {
               Author_Id=Author_Id,
               Comment_Text=Comment_Text,
               Dislikes = new List<DislikeDTO>(),
               Likes = new List<LikesDTO>(),
               Create = new BsonTimestamp(DateTime.UtcNow.ToBinary()),
               Modify = new BsonTimestamp(DateTime.UtcNow.ToBinary()),
               });
        }

        public void CreatePost(int Author_Id, string Title, string Body, List<string> Tags)
        {
            this._postDal.CreatePost(new PostsDTO
            {
                Author_Id = Author_Id,
                Title = Title,
                Body = Body,
                Tags = Tags,
                Comments = new List<CommentsDTO>() { },
                Dislikes = new List<DislikeDTO>(),
                Likes = new List<LikesDTO>(),
                Create = new BsonTimestamp(DateTime.UtcNow.ToBinary()),
                Modify = new BsonTimestamp(DateTime.UtcNow.ToBinary())  });
        }

        public void DislikePost(int PostId,int UserId)
        {
            bool has_like = false;
            bool has_dislike = false;
            PostsDTO post;
            try
            {
                post = this._postDal.GetPostById(PostId);

            }
            catch (Exception)
            {
                return;
            }
            foreach (var l in post.Likes)
            {
                if (l.User_Id == UserId)
                {
                    has_like = true;
                    break;
                }
            }
            foreach (var l in post.Dislikes)
            {
                if (l.User_Id == UserId)
                {
                    has_dislike = true;
                    break;
                }
            }
            if (has_like)
            {
                this._postDal.UnLike(PostId, new LikesDTO() { User_Id = UserId });
            }
            if (!has_dislike)
            {
                DTO.DislikeDTO dislike = new DTO.DislikeDTO() { User_Id = PostId };
                this._postDal.Dislike(PostId, dislike);
            }
        }

        public List<PostsDTO> GetAllPosts()
        {
            return this._postDal.GetAllPosts();
        }

        public PostsDTO GetPostById(int post_id)
        {
            return this._postDal.GetPostById(post_id);
        }

        public void LikePost(int PostId, int UserId)
        {
            bool has_like = false;
            bool has_dislike = false;
            PostsDTO post;
            try
            {
                post = this._postDal.GetPostById(PostId);

            }
            catch (Exception)
            {
                return;
            }
            foreach (var l in post.Likes)
            {
                if (l.User_Id == UserId)
                {
                    has_like = true;
                    break;
                }
            }
            foreach (var l in post.Dislikes)
            {
                if (l.User_Id == UserId)
                {
                    has_dislike = true;
                    break;
                }
            }

            if (has_dislike)
            {
                this._postDal.UnDislike(PostId, new DislikeDTO() { User_Id = UserId });
            }
            if (!has_like)
            {
                DTO.LikesDTO like = new DTO.LikesDTO() { User_Id = UserId };
                this._postDal.Like(PostId, like);
            }
        }
    }
}
