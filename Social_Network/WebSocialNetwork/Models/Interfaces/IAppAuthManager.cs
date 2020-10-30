using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace WebSocialNetwork.Models.Interfaces
{
    public interface IAppAuthManager
    {
        bool Register(RegisterModel u);
        bool Login(LoginModel l);
    }
}
