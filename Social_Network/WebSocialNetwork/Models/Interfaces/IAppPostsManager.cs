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
        void LikePost(int PostId,int UserId);
        void DislikePost(int PostId, int UserId);
        void AddCommentToPost(int PostId,int UserId, string CommentText);
        void CreatePost(int UserId,PostModel post);
    }
}
