using AutoMapper;
using BuisnesLogic.Interfaces;
using DTO;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSocialNetwork.Models.Concrete;
using WebSocialNetwork.Models.Interfaces;

namespace WebSocialNetwork.Models.Profiles
{
    public class UserProfile : Profile
    {
        private readonly IUserManager _userManager;
        public UserProfile(IUserManager userManager)
        {
            this._userManager = userManager;
            CreateMap<UsersDTO, UserModel>()
                .ForMember(dest => dest.User_Id, scr => scr.MapFrom(u => u.User_Id))
                .ForMember(dest => dest.User_Name, scr => scr.MapFrom(u => u.User_Name))
                .ForMember(dest => dest.User_Last_Name, scr => scr.MapFrom(u => u.User_Last_Name))
                .ForMember(dest => dest.Interests, scr => scr.MapFrom(u => u.Interests)).ReverseMap();
            CreateMap<UserLableDTO,UserModel>()
                .ForMember(dest => dest.User_Id, scr => scr.MapFrom(u => u.User_Id))
                .ForMember(dest => dest.User_Name, scr => scr.MapFrom(u => u.User_Name))
                .ForMember(dest => dest.User_Last_Name, scr => scr.MapFrom(u => u.User_Last_Name))
                .ForMember(dest => dest.Interests, scr => scr.MapFrom(u =>GetUserInterests(u.User_Id))).ReverseMap();

        }
        public List<string> GetUserInterests(int id)
        {
             var user =_userManager.GetUserById(id);
            return user.Interests;
        }

    }
}