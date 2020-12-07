using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNetwork.Models.Interfaces
{
    public interface IAppPostsManager
    {
        List<PostModel> GetAllPosts();
        List<PostModel> GetUserStream(long id);
        void LikePost(Guid PostId, int UserId);
        void DislikePost(Guid PostId, int UserId);
        void AddCommentToPost(Guid PostId, int UserId, string CommentText);
        void CreatePost(int UserId, PostModel post);
    }
}
