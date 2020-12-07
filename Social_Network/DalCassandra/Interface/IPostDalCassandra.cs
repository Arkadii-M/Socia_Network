using Cassandra;
using DTOCassandra;
using DTOCassandra.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Interface
{
    public interface IPostDalCassandra
    {
        List<PostDTO> GetAllPosts();
        PostDTO AddPost(PostDTO post);

        PostDTO GetPostById(TimeUuid id);

        PostDTO UpdatePost(PostDTO post);

        PostDTO AddCommentToPost(Guid Post_Id,Comment comment);
        PostDTO AddCommentToPost(PostDTO post, Comment comment);

        void LikePost(Guid Post_Id, long User_Id);
        void DislikePost(Guid Post_Id, long User_Id);
    }
}
