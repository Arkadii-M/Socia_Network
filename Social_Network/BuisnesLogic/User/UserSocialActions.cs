using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.User
{
    public partial class User
    {
        public void Create_Post(string Title,string Body,string Tags)
        {
            DTO.PostsDTO post = new DTO.PostsDTO() { 
                Title = Title,
                Body = Body,
                Tags = Tags.Split().ToList(),
                Create = DateTime.Now,
                Modify = DateTime.Now,
                Comment_List = new List<CommentsDTO>(),
                Likes =0,
                Dislikes =0
                    };
            DAL.Concrete.PostsDal dal = new DAL.Concrete.PostsDal(conn);
            try
            {

                dal.CreatePost(post);
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }
        public void ModifyPost(int post_id, string Title = null, string Body = null, string Tags = null)
        {
            DTO.PostsDTO post = new DTO.PostsDTO()
            {
                Title = Title,
                Body = Body,
                Tags = (Tags == null)? null: Tags.Split().ToList(),
                Modify = DateTime.UtcNow,
            };
            DAL.Concrete.PostsDal dal = new DAL.Concrete.PostsDal(conn);
            dal.UpdatePost(post);
        }
        public bool Delete_Post(int id)
        {
            DAL.Concrete.PostsDal dal = new DAL.Concrete.PostsDal(conn);
            PostsDTO post;
            try
            {
                post = dal.GetPostById(id);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            
            if(post.Author_Id == this.Account.User_Id)
            {
                try
                {
                    dal.DeletePost(id);
                }
                catch(Exception exp)
                {
                    throw exp;
                }
                
                return true;
            }
            return false;
        }
        public void LikePost(int id) 
        {
            DAL.Concrete.PostsDal dal = new DAL.Concrete.PostsDal(conn);
            DTO.LikesDTO like = new DTO.LikesDTO() { User_Id = this.Account.User_Id };
            dal.Like(id,like);
        }
        public void DislikePost(int id)
        {
            DAL.Concrete.PostsDal dal = new DAL.Concrete.PostsDal(conn);
            DTO.DislikeDTO dislike = new DTO.DislikeDTO() { User_Id = this.Account.User_Id };
            dal.Dislike(id,dislike);
        }
        public void AddToFriend(int id)
        {
            if(this.Logined)
            {
                if(!this.Account.Friends_Ids.Contains(id))
                {
                    this.Account.Friends_Ids.Add(id);
                    DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
                    dal.UpdateUser(this.Account);
                }
            }
        }
        public void DropToFriend(int id)
        {
            if (this.Logined)
            {
                if (this.Account.Friends_Ids.Contains(id))
                {
                    this.Account.Friends_Ids.Remove(id);
                    DAL.Concrete.UsersDal dal = new DAL.Concrete.UsersDal(conn);
                    dal.UpdateUser(this.Account);
                }
            }
        }

        public void AddComment(int post_id,string text)
        {
            CommentsDTO comment = new CommentsDTO()
            {
                Author_Id = this.Account.User_Id,
                Comment_Text = text,
                Likes = null,
                Dislikes = null,
                Create = DateTime.UtcNow.ToString(),
                Modify = DateTime.UtcNow.ToString()

            };
            DAL.Concrete.PostsDal dal = new DAL.Concrete.PostsDal(conn);
            //todo
        }
        public void DeleteComment(int post_id,int comment_id)
        {
            //todo
        }
    }
}
