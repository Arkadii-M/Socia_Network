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
        private readonly IAppPostsManager _postManager;
        // GET: Posts
        public PostsController(IAppPostsManager postManager)
        {
            this._postManager = postManager;
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
            return Redirect("~/Posts/Posts");
        }
        
        [HttpPost]
        public ActionResult Posts(FormCollection form)
        {
            var post_id = Guid.Parse(form.GetValues("Post_Id")[0]); //?
            var action = form.GetKey(1);

            if (action == "LikeButton")
            {
                _postManager.LikePost(post_id, user.User_Id);
            }
            else if (action == "DislikeButton")
            {
                _postManager.DislikePost(post_id, user.User_Id);
            }
            else if (action == "CommentButton")
            {
                TempData["post_id"] = post_id;
                return Redirect("~/Posts/AddComment");
            }
            ViewBag.Posts = _postManager.GetUserStream(user.User_Id);
            return View();
        }
        
        [HttpGet]
        public ActionResult Posts()
        {

            ViewBag.Posts = _postManager.GetUserStream(user.User_Id);

            return View();
        }
        [HttpGet]
        public ActionResult AddComment()
        {
            Guid id = (Guid)TempData["post_id"];
            TempData["post_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            _postManager.AddCommentToPost((Guid)TempData["post_id"], user.User_Id, comment);
            return Redirect("~/Posts/Posts");
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPost(PostModel post)
        {
            _postManager.CreatePost(user.User_Id, post);
            return Redirect("~/Posts/Posts");
        }

        public ActionResult Post()
        {
            return PartialView();
        }
    }
}
