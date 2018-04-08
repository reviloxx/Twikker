using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twikker.Data;
using Twikker.Data.Models;
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
            var posts = this.posts.GetAll();

            var postModel = posts
                .Select(result => new PostModel
                {
                    CreationDate = result.CreationDate,
                    Text = result.Content,
                    Creator = this.users.GetById(result.UserId).NickName,       
                    CreatorId = result.UserId,
                    TextId = result.TextId
                });

            var model = new IndexModel() { Posts = postModel };

            //var listingPosts = posts
            //    .Select(result => new HomeModel
            //    {
            //         Posts = result
            //    });

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserAccountModel user)
        {
            if (ModelState.IsValid)
            {
                this.users.Add(new Data.Models.User()
                {
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NickName = user.NickName,
                    Password = user.Password
                });

                ModelState.Clear();
                ViewBag.Message = user.NickName + " is successfully registered!";
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserAccountModel user)
        {
            var account = this.users.GetByNickname(user.NickName);

            if (account?.Password == user.Password)
            {
                HttpContext.Session.SetString("UserId", account.UserId.ToString());
                HttpContext.Session.SetString("Nickname", account.NickName.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.TryAddModelError("", "Nickname or password is wrong.");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(AddPostModel post)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            this.posts.Add(new Data.Models.Post()
            {
                Content = post.Text,
                UserId = userId,
                CreationDate = DateTime.Now
            });

            return RedirectToAction("Index");
        }

        public IActionResult DeletePost(int id)
        {
            this.posts.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
