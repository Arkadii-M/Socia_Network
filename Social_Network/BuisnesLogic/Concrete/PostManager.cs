using BuisnesLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;
using DalCassandra.Concrete;
using DalCassandra.Interface;
using DTO;
using DTOCassandra;
using DTOCassandra.UDT;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalNeo4j;
using DalNeo4j.Interfaces;

namespace BuisnesLogic.Concrete
{
    public class PostManager: IPostManager
    {
        private readonly IPostsDal _postDal;
        private readonly IPostDalCassandra _postDalCassandra;
        private readonly IUserStreamDalCassandra _userStreamDalCassandra;
        private readonly IUsersDalNeo4j _usersDalNeo4J;
        public PostManager(IPostsDal userDal, IPostDalCassandra postDalCassandra,IUserStreamDalCassandra userStreamDalCassandra,IUsersDalNeo4j usersDalNeo4J)
        {
            this._postDal = userDal;
            this._postDalCassandra = postDalCassandra;
            this._userStreamDalCassandra = userStreamDalCassandra;
            this._usersDalNeo4J = usersDalNeo4J;
        }


        public void AddCommentToPost(Guid Post_Id, long Author_Id, string Body)
        {
            var post = this._postDalCassandra.AddCommentToPost(Post_Id, 
                            new DTOCassandra.UDT.Comment { Body = Body, User_Id = Author_Id, Create_Date = DateTimeOffset.Now, Modify_Date = DateTimeOffset.Now });

            this._userStreamDalCassandra.UpdateStreamPost(post, GetAllFriendsForUser(post.Author_Id));
        }
        private List<long> GetAllFriendsForUser(long id)
        {
            var users_list = this._usersDalNeo4J.GetAllFriendsIdForUser((int)id);
            var u_l = new List<long>();
            foreach (var u in users_list)
            {
                u_l.Add((int)u);
            }
            u_l.Add(id);
            return u_l;
        }

        public void CreatePost(long Author_Id, string Title, string Body)
        {
            var post = this._postDalCassandra.AddPost(
                new PostDTO()
                {
                    Author_Id = Author_Id,
                    Body = Body,
                    Title = Title,
                    Comments = new List<Comment>(),
                    Create_Date = DateTimeOffset.Now,
                    Modify_Date = DateTimeOffset.Now,
                    Likes = new List<long>(),
                    Dislikes = new List<long>()
                });
            this._userStreamDalCassandra.AddPostToUsersStreams(post, GetAllFriendsForUser(Author_Id));
        }

       
        public void DislikePost(Guid PostId, int UserId)
        {
            this._postDalCassandra.DislikePost(PostId, UserId);
            this._userStreamDalCassandra.UpdateStreamPost(this._postDalCassandra.GetPostById(PostId), GetAllFriendsForUser(UserId));
        }


        public DTOCassandra.PostDTO GetPostById(Guid post_id)
        {
            return this._postDalCassandra.GetPostById(post_id);
        }

        public void LikePost(Guid PostId, int UserId)
        {
            this._postDalCassandra.LikePost(PostId, UserId);
            this._userStreamDalCassandra.UpdateStreamPost(this._postDalCassandra.GetPostById(PostId), GetAllFriendsForUser(UserId));
        }

        List<PostDTO> IPostManager.GetAllPosts()
        {
            return this._postDalCassandra.GetAllPosts();
        }
    }
}
