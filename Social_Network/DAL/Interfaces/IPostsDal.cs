using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL.Interfaces
{
    public interface IPostsDal
    {
        PostsDTO GetPostById(int id);
        List<PostsDTO> GetAllPosts();
        PostsDTO UpdatePost(PostsDTO post);
        PostsDTO CreatePost(PostsDTO post);
        void DeletePost(int id);
        void Like(int post_id,LikesDTO like);
        void Dislike(int post_id, DislikeDTO like);
        void AddCommentToPost(int id, CommentsDTO comment);

    }
}
