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
        private IUserText userTexts;
        private IReaction reactions;

        public HomeController(IUser users, IPost posts, IComment comments,IUserText userTexts, IReaction reactions)
        {
            this.users = users;
            this.posts = posts;
            this.comments = comments;
            this.userTexts = userTexts;
            this.reactions = reactions;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("posts/get")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetPosts()
        {
            var posts = this.posts.GetAll();

            var postModel = posts
                .Select(post => new PostModel
                {
                    PostId = post.PostId,
                    UserTextId = post.UserTextId,
                    CreatorId = post.CreatorId,
                    CreatorNickname = this.users.GetById(post.CreatorId).NickName,
                    CreationDate = post.CreationDate.ToString("dd.MM.yyyy, H:mm:ss"),
                    Content = post.Content,
                    Reactions = this.reactions.GetAll(post.UserTextId)?
                    .Select(reaction => new ReactionModel
                    {
                        Reaction = ReactionType.like,
                        CreatorId = reaction.Creator.UserId
                    }),
                    Comments = this.comments.GetByPostId(post.PostId)?
                        .Select(comment => new CommentModel
                        {
                            CommentId = comment.CommentId,
                            UserTextId = comment.UserTextId,
                            CreatorId = comment.CreatorId,
                            CreatorNickname = this.users.GetById(comment.CreatorId).NickName,
                            CreationDate = comment.CreationDate.ToString("dd.MM.yyyy, H:mm:ss"),
                            Content = comment.Content,
                            Reactions = this.reactions.GetAll(comment.UserTextId)?
                            .Select(reaction => new ReactionModel
                            {
                                Reaction = ReactionType.like,
                                CreatorId = reaction.Creator.UserId
                            })
                        }),                    
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
            var userText = new Data.Models.UserText();

            this.posts.Add(new Data.Models.Post()
            {
                Content = post.Content,
                UserText = userText,
                Creator = this.users.GetById(userId),
                UserTextId = userText.UserTextId,
                CreationDate = DateTime.Now                
            });

            return Content("Success :");
        }       

        [Route("posts/delete")]
        [HttpPost]
        public IActionResult DeletePost(DeletePostModel model)
        {
            int userTextId = this.posts.GetById(model.PostId).UserTextId;

            try
            {
                this.reactions.RemoveByTextId(userTextId);
                this.reactions.Remove(userTextId);
            }
            catch
            {
                return Json(new JSONResponse(false));
            }
            
            return Json(new JSONResponse(true));
        }

        [Route("comments/add")]
        [HttpPost]
        public IActionResult AddComment(AddCommentModel comment)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var userText = new Data.Models.UserText();

            this.comments.Add(new Data.Models.Comment()
            {
                CreatorId = userId,
                Content = comment.Content,
                UserText = userText,
                Post = this.posts.GetById(comment.PostId),
                Creator = this.users.GetById(userId),
                CreationDate = DateTime.Now
            });

            return Content("Success :");
        }

        [Route("comments/delete")]
        [HttpPost]
        public IActionResult DeleteComment(DeleteCommentModel comment)
        {
            this.comments.Remove(comment.CommentId);

            return Content("Success :");
        }

        [Route("reactions/add")]
        [HttpPost]
        public IActionResult AddReaction(AddReactionModel reaction)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            this.reactions.Add(new Data.Models.Reaction()
            {
                Creator = this.users.GetById(userId),
                CreationDate = DateTime.Now,
                UserText = this.userTexts.GetById(reaction.TextId),
                ReactionType = ReactionType.like
            });
            return Content("Success :");
        }
    }
}
