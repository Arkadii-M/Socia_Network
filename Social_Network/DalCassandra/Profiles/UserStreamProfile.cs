using AutoMapper;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCassandra.Profiles
{
    public class UserStreamProfile:Profile
    {
        public UserStreamProfile()
        {
            CreateMap<List<UserStream>, UserStreamDTO>()
                .ForMember(dest => dest.User_Id, scr => scr.MapFrom(p => p.Count != 0 ? p[0].User_Id : 0))
                .ForMember(dest => dest.Stream, scr => scr.MapFrom(p => ConvertToPosts(p)));



        }
        private List<PostDTO> ConvertToPosts(List<UserStream> stream)
        {
            var ret = new List<PostDTO>();
            foreach(var p in stream)
            {
                ret.Add( new PostDTO()
                {
                    Title = p.Title,
                    Body = p.Body,
                    Comments = p.Comments,
                    Create_Date = p.Create_Date,
                    Dislikes = p.Dislikes,
                    Likes = p.Likes,
                    Modify_Date = p.Modify_Date,
                    Author_Id = p.Author_Id,
                    Post_Id = p.Post_Id
                });
            }

            return ret;
            

        }
    }
}
