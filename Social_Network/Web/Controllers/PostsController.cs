using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PostsController : Controller
    {
        private static UserContent user;
        // GET: Posts

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
            return Redirect("~/Posts/Posts");
        }
        
        [HttpPost]
        public ActionResult Posts(FormCollection form)
        {
            var keys = form.GetKey(0);
            var val = form.GetValues(keys);
            int id = Convert.ToInt32(keys);

            if (val[0] == "Like")
            {
                user.Get().LikePost(id);
            }
            else if (val[0] == "Dislike")
            {
                user.Get().DislikePost(id);
            }
            else if (val[0] == "Comment")
            {
                TempData["post_id"] = id;
                return Redirect("~/Posts/AddComment");
            }
            ViewBag.Posts = user.Get().GetAllPosts();
            return View();
        }
        
        [HttpGet]
        public ActionResult Posts()
        {
            ViewBag.Posts = user.Get().GetAllPosts();
            return View();
        }
        [HttpGet]
        public ActionResult AddComment()
        {
            // int id = (int)TempData["post_id"];
            //TempData["post_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            user.Get().AddComment((int)TempData["post_id"], comment);
            return Redirect("~/Posts/Posts");
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            // int id = (int)TempData["post_id"];
            //TempData["post_id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddPost(PostsDTO post)
        {
            user.Get().Create_Post(post.Title, post.Body, string.Join(",",post.Tags));
            return Redirect("~/Posts/Posts");
        }
    }
}
