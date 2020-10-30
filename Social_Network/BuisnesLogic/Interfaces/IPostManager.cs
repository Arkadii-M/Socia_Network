using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic.Interfaces
{
    public interface IPostManager
    {
        List<PostsDTO> GetAllPosts();
        PostsDTO GetPostById(int post_id);
        void LikePost(int PostId, int UserId);
        void DislikePost(int PostId, int UserId);
        void CreatePost(int Author_Id,string Title,string Body,List<string> Tags);
        void AddCommentToPost(int PostId, int Author_Id,string Comment_Text);
    }
}
