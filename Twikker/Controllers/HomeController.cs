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

        public HomeController(IUser users, IPost posts)
        {
            this.users = users;
            this.posts = posts;
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
                .Select(result => new PostModel
                {
                    CreationDate = result.CreationDate.ToString("dd.MM.yyyy, H:mm"),
                    Content = result.Content,
                    PostId = result.PostId,
                    CreatorId = result.CreatorUserId,
                    CreatorNickname = this.users.GetById(result.CreatorUserId).NickName
                }).OrderByDescending(p => p.CreationDate);
            
            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);

            var model = new IndexModel()
            {
                Posts = postModel,
                activeUserId = loggedIn ? activeUserId : -1
            };

            return Json(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        

        public IActionResult AddPost()
        {
            return View();
        }

        [Route("posts/add")]
        [HttpPost]
        public IActionResult AddPost(AddPostModel post)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            this.posts.Add(new Data.Models.Post()
            {
                Content = post.Text,
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
    }
}
