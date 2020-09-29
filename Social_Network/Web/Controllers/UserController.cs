using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Controllers
{
    public class UserContent
    {
       private BuisnesLogic.User.User user;
        public UserContent()
        {
            this.user = null;
        }
        public void Set(BuisnesLogic.User.User u)
        {
            this.user = u;
        }
        public BuisnesLogic.User.User Get()
        {
            return this.user;
        }
    }
}