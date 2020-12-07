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
        string User_Login { get; set; }
        bool IsLogined { get; set; }
    }
}
