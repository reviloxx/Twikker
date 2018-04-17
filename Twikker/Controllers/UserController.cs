using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twikker.Data;
using Twikker.Web.Models;

namespace Twikker.Web.Controllers
{
    public class UserController : Controller
    {
        private IUser users;

        public UserController(IUser users)
        {
            this.users = users;
        }

        [Route("register")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Register(UserAccountModel user)
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

            return Content("Success :");
        }

        [Route("login")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Login(UserAccountModel user)
        {
            var account = this.users.GetByNickname(user.NickName);

            if (account?.Password == user.Password)
            {
                HttpContext.Session.SetString("UserId", account.UserId.ToString());
                HttpContext.Session.SetString("Nickname", account.NickName.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.TryAddModelError("", "Nickname or password is wrong.");
            }

            return Content("Success :");
        }

        [Route("logout")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);
            return Json(loggedIn ? activeUserId : -1);
        }
    }
}