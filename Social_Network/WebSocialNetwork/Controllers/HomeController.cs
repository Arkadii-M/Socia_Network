using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using WebSocialNetwork.Models;

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
            AppUser user = new AppUser();
            if (user.Login(l.UserName,l.Password))
            {
                TempData["User"] = user.User_Id;
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
            AppUser user = new AppUser();

            if (user.Register(u))
            {
                TempData["User"] = user.User_Id;
                return RedirectToRoute(new { controller = "RegisteredUser", action = "Index" });
            }
            return new HttpUnauthorizedResult();
        }
    }
}
