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
        public IActionResult GetActive()
        {
            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);
            UserModel userAccountModel = new UserModel();

            if (loggedIn)
            {
                var user = this.users.GetById(activeUserId);
                userAccountModel = new UserModel()
                {
                    UserId = activeUserId,
                    NickName = user.NickName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth
                };
            }
            
            return Json(userAccountModel);
        }

        [Route("user/register")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Register(UserModel user)
        {           
            if (!this.regexUtilities.IsValidNickname(user.NickName))
            {
                return Json(new JSONResponse(false, "Invalid Nickname! Must contain 3 to 20 characters."));
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

            return Json(new JSONResponse(true, "You are successfully registered as " + user.NickName + "!"));
        }

        [Route("user/update")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Update(UserModel user)
        {
            if (user.FirstName == null)
            {
                user.FirstName = string.Empty;
            }

            if (user.LastName == null)
            {
                user.LastName = string.Empty;
            }

            if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId))
            {
                return Json(new JSONResponse(false, "Unexpected Error"));
            }

            if (!this.regexUtilities.IsValidNickname(user.NickName))
            {
                return Json(new JSONResponse(false, "Invalid Nickname! Must contain 3 to 20 characters."));
            }

            if (!this.regexUtilities.IsValidEmail(user.Email))
            {
                return Json(new JSONResponse(false, "Invalid Email address!"));
            }

            if (this.users.GetByEmail(user.Email) != null && this.users.GetById(activeUserId).Email != user.Email)
            {
                return Json(new JSONResponse(false, "This Email address is already in use!"));
            }

            this.users.Update(new Data.Models.User()
            {
                UserId = activeUserId,
                NickName = user.NickName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Password = this.users.GetById(activeUserId).Password
            });

            return Json(new JSONResponse(true));
        }

        [Route("user/login")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Login(UserModel user)
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