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
        private readonly IAppAuthManager _authManager;
        private readonly IAppUserManager _userManager;
        public HomeController(IAppAuthManager authManager, IAppUserManager userManager)
        {
            this._authManager = authManager;
            this._userManager = userManager;
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
            if (_authManager.Login(l))
            {
                TempData["User"] = _userManager.GetUserByLogin(l.UserName).User_Id;
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
            if (_userManager.CreateUser(u))
            {
                TempData["User"] = _userManager.GetUserByLogin(u.User_Login).User_Id;
                return RedirectToRoute(new { controller = "RegisteredUser", action = "Index" });
            }
            return new HttpUnauthorizedResult();
        }
    }
}
