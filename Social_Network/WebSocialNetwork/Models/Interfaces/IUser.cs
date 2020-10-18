using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocialNetwork.Models
{
    public interface IUser
    {
        int User_Id { get; set; }
        BuisnesLogic.User.User Get();
        void Set(int id);
    }
}
