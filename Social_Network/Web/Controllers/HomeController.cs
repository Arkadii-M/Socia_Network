using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using DAL;
using Web.Models;
using System.ComponentModel;
using BuisnesLogic.User;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static UserContent user;
        //private UserContext db = new UserContext();
        public HomeController()
        {
            user = null;
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
            user = new UserContent();
            user.Set(new BuisnesLogic.User.User());
            if (user.Get().LoginAsUser(l.UserName,l.Password))
            {
                TempData["User"] = user;
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
        public ActionResult Register(UsersDTO u)
        {
            user.Set(new BuisnesLogic.User.User());
            if (user.Get().CreateNewUser(u.User_Login,u.User_Password,u.User_Name,u.User_Last_Name,u.Email,u.Interests))
            {
                TempData["User"] = user;
                return RedirectToRoute(new { controller = "RegisteredUser", action = "Index" });
            }
            return new HttpUnauthorizedResult();
        }
    }
}