using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twikker.Data;
using Twikker.Data.Models;
using Twikker.Web;
using Twikker.Web.Models;

namespace Twikker.Controllers
{
    public class HomeController : Controller
    {
        private IUser users;
        private IPost posts;
        private IComment comments;

        public HomeController(IUser users, IPost posts, IComment comments)
        {
            this.users = users;
            this.posts = posts;
            this.comments = comments;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("getActiveUserId")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetActiveUserId()
        {
            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);
            return Json(loggedIn ? activeUserId : -1);
        }


        [Route("getPosts")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetPosts()
        {
            var posts = this.posts.GetAll();

            var postModel = posts
                .Select(post => new PostModel
                {
                    PostId = post.PostId,
                    CreatorId = post.CreatorId,
                    CreatorNickname = this.users.GetById(post.CreatorId).NickName,
                    CreationDate = post.CreationDate.ToString("dd.MM.yyyy, H:mm"),
                    Content = post.Content,
                    Comments = this.comments.GetByPostId(post.PostId)?
                        .Select(comment => new CommentModel
                        {
                            CommentId = comment.CommentId,
                            CreatorId = comment.CreatorId,
                            CreatorNickname = this.users.GetById(comment.CreatorId).NickName,
                            CreationDate = comment.CreationDate.ToString("dd.MM.yyyy, H:mm"),
                            Content = comment.Content
                        })
                }).OrderByDescending(p => p.CreationDate);
            

            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);

            var model = new IndexModel()
            {
                Posts = postModel,
                activeUserId = loggedIn ? activeUserId : -1
            };

            return Json(model);
        }

        [Route("posts/add")]
        [HttpPost]
        public IActionResult AddPost(AddPostModel post)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            this.posts.Add(new Data.Models.Post()
            {
                Content = post.Content,
                Creator = this.users.GetById(userId),
                CreationDate = DateTime.Now                
            });

            return Content("Success :");
        }

        [Route("posts/delete")]
        [HttpPost]
        public IActionResult DeletePost(DeletePostModel model)
        {
            try
            {
                this.posts.Remove(model.PostId);
            }
            catch
            {
                return Json(new JSONResponse(false));
            }
            
            return Json(new JSONResponse(true));
            //return RedirectToAction("Index");
        }

        [Route("comments/add")]
        [HttpPost]
        public IActionResult AddComment(AddCommentModel comment)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            this.comments.Add(new Data.Models.Comment()
            {
                CreatorId = userId,
                Content = comment.Content,
                Post = this.posts.GetById(comment.PostId),
                PostId = comment.PostId,
                Creator = this.users.GetById(userId),
                CreationDate = DateTime.Now
            });

            return Content("Success :");
        }
    }
}
