using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSocialNetwork.Models.Interfaces;
using BuisnesLogic.Concrete;
using AutoMapper;
using BuisnesLogic.User;
using DTO;
using DTONeo4j;

namespace WebSocialNetwork.Models.Concrete
{
    public class AppUserManager: IAppUserManager
    {
        private readonly IMapper _mapper;
        public AppUserManager()
        {
            MapperConfiguration conf = new MapperConfiguration(
                    cfg => cfg.AddMaps(
                        typeof(UsersDTO).Assembly,
                        typeof(UserLableDTO).Assembly,
                        typeof(MyPageUserModel).Assembly
                    )
                );
            this._mapper = conf.CreateMapper();
        }
        
        public AppUserManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<UserModel> GetAllUsers()
        {
            BuisnesLogic.Concrete.UserManager manager = new UserManager();
            return _mapper.Map<List<UserModel>>(manager.GetAllUsers());

        }

        public UsersPathModel GetPathBetweenUsers(int u1_id, int u2_id)
        {
            BuisnesLogic.Concrete.UserManager manager = new UserManager();
            var path = manager.GetPathBetweenUsers(u1_id, u2_id);
            var path_list = _mapper.Map<List<UserModel>>(path);
            return new UsersPathModel { UserFromId = u1_id, UserToId = u2_id, PathToUser = path_list };
        }

        public int GetPathLenBetweenUsers(int id_1, int id_2)
        {
            BuisnesLogic.Concrete.UserManager manager = new UserManager();
           return  manager.GetPathLenBetweenUsers(id_1, id_2);
        }

        public UserModel GetUserById(int id)
        {
            BuisnesLogic.Concrete.UserManager manager = new UserManager();
            return _mapper.Map<UserModel>(manager.GetUserById(id));
        }
        public MyPageUserModel GetMyUserById(int id)
        {
            BuisnesLogic.Concrete.UserManager manager = new UserManager();
            return _mapper.Map<MyPageUserModel>(manager.GetUserById(id));
        }

        public UserModel GetUserByLogin(string login)
        {
            BuisnesLogic.Concrete.UserManager manager = new UserManager();
            return _mapper.Map<UserModel>(manager.GetUserByLogin(login));
        }
    }
}