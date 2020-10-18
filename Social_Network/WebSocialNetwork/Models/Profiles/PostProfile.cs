using AutoMapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocialNetwork.Models.Profiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            
            CreateMap<PostsDTO, PostModel>()
                .ForMember(dest => dest.Post_Id, scr => scr.MapFrom(m => m.Post_Id))
                .ForMember(dest => dest.Title, scr => scr.MapFrom(m => m.Title))
                .ForMember(dest => dest.Body, scr => scr.MapFrom(m => m.Body))
                .ForMember(dest => dest.Tags, scr => scr.MapFrom(m => m.Tags))
                .ForMember(dest => dest.Likes, scr => scr.MapFrom(m => m.Likes))
                .ForMember(dest => dest.Dislikes, scr => scr.MapFrom(m => m.Dislikes))
                .ForMember(dest => dest.Comments, scr => scr.MapFrom(m => m.Comments))
                .ForMember(dest => dest.Author_FullName, scr => scr.MapFrom(name => GetUserFullName(name.Author_Id)));
                
            //CreateMap<PostModel, PostsDTO>().ReverseMap();

        }

        public static string GetUserFullName(int id)
        {
            AppUser u = new AppUser(id);
            var name = u.Get().GetMyName();
            var last_name = u.Get().GetMyLastName();
            return (name+" "+last_name);
        }
    }
}