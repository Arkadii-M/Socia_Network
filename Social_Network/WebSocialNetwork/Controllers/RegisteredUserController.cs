using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using System.ComponentModel;
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
        private readonly IAppUserManager _userManager;
        public RegisteredUserController(IAppUserManager userManager)
        {
            this._userManager = userManager;
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
            ViewBag.User = _userManager.GetMyUserById(user.User_Id);
            return View();
        }
        public ActionResult Users()
        {
            var all_users = _userManager.GetAllUsers();
            ViewBag.Users = all_users;
            return View();
        }
        [HttpGet]
        public ActionResult UserPage(int id)
        {
            var userinfo = _userManager.GetUserById(id);
            var path = _userManager.GetPathBetweenUsers(user.User_Id,id);
            ViewBag.Path = path;
            ViewBag.User = userinfo;
            TempData["User_Id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult UserPage()
        {   
            int id = (int)TempData["User_Id"];

            _userManager.AddToFriend(user.User_Id, id);

            return Redirect("~/RegisteredUser/Users");
        }

        public ActionResult Posts()
        {
            TempData["User"] = user.User_Id;
            return RedirectToRoute(new { controller = "Posts", action = "Index" });
        }

    }
}