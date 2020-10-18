using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNetwork.Models.Interfaces
{
    public interface IAppUserManager
    {
        List<UserModel> GetAllUsers();
        UserModel GetUserByLogin(string login);
        UserModel GetUserById(int id);

        MyPageUserModel GetMyUserById(int id);
        int GetPathLenBetweenUsers(int id_1, int id_2);
        UsersPathModel GetPathBetweenUsers(int u1_id, int u2_id);

    }
}
