using BuisnesLogic.Concrete;
using BuisnesLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
using WebSocialNetwork.Models.Interfaces;

namespace WebSocialNetwork.Models.Concrete
{
    public class AppAuthManager : IAppAuthManager
    {
        private readonly IAuthManager _authManager;
        public AppAuthManager(IAuthManager authManager)
        {
            this._authManager = authManager;
        }
        public AppAuthManager()
        {
            this._authManager = new AuthManager();
        }

        public bool Login(LoginModel l)
        {
            return this._authManager.Login(l.UserName, l.Password);
        }

        public bool Register(RegisterModel u)
        {
            throw new NotImplementedException();
        }
    }
}
