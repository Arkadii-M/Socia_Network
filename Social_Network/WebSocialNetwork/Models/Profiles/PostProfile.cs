using AutoMapper;
using BuisnesLogic.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebSocialNetwork.Models.Concrete;
using WebSocialNetwork.Models.Interfaces;

namespace WebSocialNetwork.Models.Profiles
{
    public class PostProfile: Profile
    {
        private readonly IUserManager _userManager;
        public PostProfile(IUserManager userManager)
        {
            this._userManager = userManager;
            CreateMap<PostsDTO, PostModel>()
                .ForMember(dest => dest.Post_Id, scr => scr.MapFrom(m => m.Post_Id))
                .ForMember(dest => dest.Title, scr => scr.MapFrom(m => m.Title))
                .ForMember(dest => dest.Body, scr => scr.MapFrom(m => m.Body))
                .ForMember(dest => dest.Tags, scr => scr.MapFrom(m => m.Tags))
                .ForMember(dest => dest.Likes, scr => scr.MapFrom(m => m.Likes))
                .ForMember(dest => dest.Dislikes, scr => scr.MapFrom(m => m.Dislikes))
                .ForMember(dest => dest.Comments, scr => scr.MapFrom(m => m.Comments))
                .ForMember(dest => dest.Author_FullName, scr => scr.MapFrom(name => GetUserFullName(name.Author_Id)));


            CreateMap<DTOCassandra.PostDTO, PostModel>()
               .ForMember(dest => dest.Post_Id, scr => scr.MapFrom(m => m.Post_Id))
               .ForMember(dest => dest.Title, scr => scr.MapFrom(m => m.Title))
               .ForMember(dest => dest.Body, scr => scr.MapFrom(m => m.Body))
               .ForMember(dest => dest.Likes, scr => scr.MapFrom(m => m.Likes))
               .ForMember(dest => dest.Dislikes, scr => scr.MapFrom(m => m.Dislikes))
               .ForMember(dest => dest.Comments, scr => scr.MapFrom(m => m.Comments))
               .ForMember(dest => dest.Author_FullName, scr => scr.MapFrom(name => GetUserFullName(name.Author_Id)));

            //CreateMap<PostModel, PostsDTO>().ReverseMap();

        }

        public string GetUserFullName(long id)
        {
            var user = _userManager.GetUserById((int)id);

            var name = user.User_Name;
            var last_name = user.User_Last_Name;
            return (name+" "+last_name);
        }
    }
}