using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
using System.Web.Mvc;
using WebSocialNetwork.Models;
using AutoMapper;
using WebSocialNetwork.Models.Profiles;
using WebSocialNetwork.Models.Interfaces;
using WebSocialNetwork.Models.Concrete;

namespace Web.Controllers
{
    public class PostsController : Controller
    {
        private static IUser user;
        // GET: Posts

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
            return Redirect("~/Posts/Posts");
        }
        
        [HttpPost]
        public ActionResult Posts(FormCollection form)
        {
            IAppPostsManager manager = new AppPostsManager();

            var post_id = Convert.ToInt32(form.GetValues("Post_Id")[0]);
            var action = form.GetKey(1);

            if (action == "LikeButton")
            {
                manager.LikePost(post_id, user.User_Id);
            }
            else if (action == "DislikeButton")
            {
               manager.DislikePost(post_id, user.User_Id);
            }
            else if (action == "CommentButton")
            {
                TempData["post_id"] = post_id;
                return Redirect("~/Posts/AddComment");
            }
            ViewBag.Posts = manager.GetAllPosts();
            return View();
        }
        
        [HttpGet]
        public ActionResult Posts()
        {
            IAppPostsManager manager = new AppPostsManager();
            ViewBag.Posts = manager.GetAllPosts();

            return View();
        }
        [HttpGet]
        public ActionResult AddComment()
        {
            int id = (int)TempData["post_id"];
            TempData["post_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            IAppPostsManager manager = new AppPostsManager();
            manager.AddCommentToPost((int)TempData["post_id"], user.User_Id, comment);
            return Redirect("~/Posts/Posts");
        }

        [HttpGet]
        public ActionResult AddPost()
        {
             //int id = (int)TempData["post_id"];
            //TempData["post_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddPost(PostModel post)
        {
            IAppPostsManager manager = new AppPostsManager();
            manager.CreatePost(user.User_Id, post);
            return Redirect("~/Posts/Posts");
        }

        public ActionResult Post()
        {
            return PartialView();
        }
    }
}
