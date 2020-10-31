using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSocialNetwork.Models.Interfaces;
using BuisnesLogic.Concrete;
using AutoMapper;
using DTO;
using DTONeo4j;
using BuisnesLogic.Interfaces;
using WebSocialNetwork.Models.Profiles;

namespace WebSocialNetwork.Models.Concrete
{
    public class AppUserManager: IAppUserManager
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        public AppUserManager(IUserManager userManager)
        {
            this._userManager = userManager;
            MapperConfiguration conf = new MapperConfiguration(
                mc => {
                    mc.AddProfile(new UserProfile(_userManager));
                    mc.AddProfile(new MyPageUserProfile());
                    mc.AddProfile(new RegisterProfile());
                    }
                ) ;
            this._mapper = conf.CreateMapper();
        }
        
        public AppUserManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<UserModel> GetAllUsers()
        {
            return _mapper.Map<List<UserModel>>(_userManager.GetAllUsers());

        }

        public UsersPathModel GetPathBetweenUsers(int u1_id, int u2_id)
        {
            var path = _userManager.GetPathBetweenUsers(u1_id, u2_id);
            var path_len = _userManager.GetPathLenBetweenUsers(u1_id, u2_id);
            var path_list = _mapper.Map<List<UserModel>>(path);
            return new UsersPathModel { UserFromId = u1_id, UserToId = u2_id, PathToUser = path_list,PathLen = path_len };
        }

        public int GetPathLenBetweenUsers(int id_1, int id_2)
        {
           return _userManager.GetPathLenBetweenUsers(id_1, id_2);
        }

        public UserModel GetUserById(int id)
        {
            return _mapper.Map<UserModel>(_userManager.GetUserById(id));
        }
        public MyPageUserModel GetMyUserById(int id)
        {
            return _mapper.Map<MyPageUserModel>(_userManager.GetUserById(id));
        }

        public UserModel GetUserByLogin(string login)
        {
            return _mapper.Map<UserModel>(_userManager.GetUserByLogin(login));
        }

        public void AddToFriend(int id_from, int id_to)
        {
            _userManager.CreateRelationship(id_from, id_to);
        }

        public bool CreateUser(RegisterModel u)
        {
            return _userManager.CreateUser(_mapper.Map<UsersDTO>(u));
        }
    }
}