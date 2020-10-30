using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using WebSocialNetwork.Models;
using WebSocialNetwork.Models.Concrete;
using WebSocialNetwork.Models.Interfaces;

namespace WebSocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel l)
        {
            IAppAuthManager auth = new AppAuthManager();
            IAppUserManager manager = new AppUserManager();

            if (auth.Login(l))
            {
                TempData["User"] = manager.GetUserByLogin(l.UserName).User_Id;
                return RedirectToRoute(new { controller = "RegisteredUser", action = "Index" });
            }
            return new HttpUnauthorizedResult();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel u)
        {

            AppUser user = new AppUser();
            IAppUserManager manager = new AppUserManager();
            
            if (manager.CreateUser(u))
            {
                TempData["User"] = manager.GetUserByLogin(u.User_Login).User_Id;
                return RedirectToRoute(new { controller = "RegisteredUser", action = "Index" });
            }
            return new HttpUnauthorizedResult();
        }
    }
}
