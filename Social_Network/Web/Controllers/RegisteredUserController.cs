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
    public class RegisteredUserController : Controller
    {
        // GET: RegisteredUser
        private static UserContent user;
        public RegisteredUserController()
        {
            
        }
        public ActionResult Index()
        {
            
            if (TempData["User"] != null)
            {
                user = (UserContent)TempData["User"];
            }
            else
            { 
                return new HttpUnauthorizedResult();
            }
            ViewBag.Users = user.Get().GetAllUsers();

            return View();
        }
        public ActionResult MyPage()
        {
            ViewBag.User = user.Get();
            return View();
        }
        public ActionResult Users()
        {
            ViewBag.Users = user.Get().GetAllUsers();
            return View();
        }
        [HttpGet]
        public ActionResult UserPage(int id)
        {
            var temp =user.Get().GetAllUsers();
            ViewBag.User = temp[id-1];
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

            TempData["User"] = user;
            return RedirectToRoute(new { controller = "Posts", action = "Index" });
        }

    }
}