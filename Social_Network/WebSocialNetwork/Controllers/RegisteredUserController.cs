using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using System.ComponentModel;
using BuisnesLogic.User;
using WebSocialNetwork.Models;
using BuisnesLogic.Interfaces;
using BuisnesLogic.Concrete;
using WebSocialNetwork.Models.Interfaces;
using WebSocialNetwork.Models.Concrete;

namespace Web.Controllers
{
    public class RegisteredUserController : Controller
    {
        // GET: RegisteredUser
        private static IUser user;
        public RegisteredUserController()
        {
        }
        public ActionResult Index()
        {
            
            if (TempData["User"] != null)
            {
                user = new AppUser((int)TempData["User"]);
            }
            else
            { 
                return new HttpUnauthorizedResult();
            }

            return Redirect("~/RegisteredUser/Users");
        }
        public ActionResult MyPage()
        {
            IAppUserManager manager = new AppUserManager();
            ViewBag.User = manager.GetMyUserById(user.User_Id);
            return View();
        }
        public ActionResult Users()
        {
            IAppUserManager manager = new AppUserManager(); 
            var all_users = manager.GetAllUsers();
            ViewBag.Users = all_users;
            return View();
        }
        [HttpGet]
        public ActionResult UserPage(int id)
        {
            IAppUserManager manager = new AppUserManager();
            var userinfo = manager.GetUserById(id);
            var path = manager.GetPathBetweenUsers(user.User_Id,id);
            ViewBag.Path = path;
            ViewBag.User = userinfo;
            TempData["User_Id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult UserPage()
        {
            int id = (int)TempData["User_Id"];

            user.Get().AddToFriend(id);
            return Redirect("~/RegisteredUser/Users");
        }

        public ActionResult Posts()
        {
            TempData["User"] = user.User_Id;
            return RedirectToRoute(new { controller = "Posts", action = "Index" });
        }

    }
}