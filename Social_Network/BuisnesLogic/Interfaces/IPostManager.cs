using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOCassandra;
namespace BuisnesLogic.Interfaces
{
    public interface IPostManager
    {
        List<DTOCassandra.PostDTO> GetAllPosts();
        DTOCassandra.PostDTO GetPostById(Guid post_id);

        void LikePost(Guid PostId, int UserId);

        void DislikePost(Guid PostId, int UserId);

        void CreatePost(long Author_Id, string Title, string Body);

        void AddCommentToPost(Guid Post_Id, long Author_Id, string Body);
    }
}
