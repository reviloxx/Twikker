using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private RegexUtilities regexUtilities;

        private static int minPasswordLength = 4;

        public UserController(IUser users)
        {
            this.users = users;
            this.regexUtilities = new RegexUtilities();
        }

        [Route("user/get")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetActiveUserId()
        {
            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);
            return Json(loggedIn ? activeUserId : -1);
        }

        [Route("user/register")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Register(UserAccountModel user)
        {           
            if (!this.regexUtilities.IsValidNickname(user.NickName))
            {
                return Json(new JSONResponse(false, "Invalid Nickname!"));
            }

            if (!this.regexUtilities.IsValidEmail(user.Email))
            {
                return Json(new JSONResponse(false, "Invalid Email address!"));
            }

            if (this.users.GetByEmail(user.Email) != null)
            {
                return Json(new JSONResponse(false, "This Email address is already in use!"));
            }

            if (!this.regexUtilities.IsValidPassword(user.Password, minPasswordLength))
            {
                return Json(new JSONResponse(false, "Password must contain at least " + minPasswordLength + " characters!"));
            }

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

            return Json(new JSONResponse(true, "user registered"));
        }

        [Route("user/login")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Login(UserAccountModel user)
        {
            var account = this.users.GetByNickname(user.NickName);

            if (account == null)
            {
                account = this.users.GetByEmail(user.NickName);
            }

            if (account?.Password == user.Password)
            {
                HttpContext.Session.SetString("UserId", account.UserId.ToString());
                HttpContext.Session.SetString("Nickname", account.NickName.ToString());
                return Json(new JSONResponse(true));
            }
            else
            {
                //ModelState.TryAddModelError("", "Nickname or password is wrong.");
                return Json(new JSONResponse(false));
            }

            
        }

        [Route("user/logout")]
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