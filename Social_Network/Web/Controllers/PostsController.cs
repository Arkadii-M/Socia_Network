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
            var a = form.AllKeys;
            var post_id = Convert.ToInt32(form.GetValues("Post_Id")[0]);
            var action = form.GetKey(1);

            if (action == "LikeButton")
            {
                user.Get().LikePost(post_id);
            }
            else if (action == "DislikeButton")
            {
                user.Get().DislikePost(post_id);
            }
            else if (action == "CommentButton")
            {
                TempData["post_id"] = post_id;
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

        public ActionResult Post()
        {
            return PartialView();
        }
    }
}
