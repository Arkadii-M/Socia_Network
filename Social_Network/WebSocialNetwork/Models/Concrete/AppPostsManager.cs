using AutoMapper;
using BuisnesLogic.Concrete;
using BuisnesLogic.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNetwork.Models.Interfaces;
using WebSocialNetwork.Models.Profiles;

namespace WebSocialNetwork.Models.Concrete
{
    public class AppPostsManager: IAppPostsManager
    {
        private readonly IMapper _mapper;
        private readonly IPostManager _postManager;
        public AppPostsManager()
        {
            this._mapper = ConfigureMapper();
            this._postManager = new PostManager();
        }
        public AppPostsManager(IPostManager postManager)
        {
            this._mapper = ConfigureMapper();
            this._postManager = postManager;
        }

        private IMapper ConfigureMapper()
        {
            var conf = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PostProfile());
            });
            return conf.CreateMapper();

        }

        public void AddCommentToPost(int PostId,int UserId, string CommentText)
        {
            this._postManager.AddCommentToPost(PostId, UserId, CommentText);
        }

        public void CreatePost(int UserId, PostModel post)
        {
            this._postManager.CreatePost(UserId, post.Title, post.Body, post.Tags);
        }

        public void DislikePost(int PostId, int UserId)
        {
            this._postManager.DislikePost(PostId, UserId);
        }

        public List<PostModel> GetAllPosts()
        {
            var all_posts = new List<PostModel>();

            foreach (var p in this._postManager.GetAllPosts())
            {
                all_posts.Add(this._mapper.Map<PostModel>(p));
            }
            return all_posts;
        }

        public void LikePost(int PostId, int UserId)
        {
            this._postManager.LikePost(PostId, UserId);
        }
    }
}
