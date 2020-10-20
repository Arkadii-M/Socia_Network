using AutoMapper;
using DTO;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSocialNetwork.Models.Concrete;

namespace WebSocialNetwork.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
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
        public static List<string> GetUserInterests(int id)
        {
            var manager = new AppUserManager();
            var user =manager.GetUserById(id);
            return user.Interests;
        }

    }
}