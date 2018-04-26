using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twikker.Data;
using Twikker.Data.Models;
using Twikker.Web;
using Twikker.Web.Models;

namespace Twikker.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> log;

        private IUser users;
        private IPost posts;
        private IComment comments;
        private IUserText userTexts;
        private IReaction reactions;

        public HomeController(ILogger<HomeController> log, IUser users, IPost posts, IComment comments,IUserText userTexts, IReaction reactions)
        {
            this.log = log;
            this.users = users;
            this.posts = posts;
            this.comments = comments;
            this.userTexts = userTexts;
            this.reactions = reactions;
        }
        
        public IActionResult Index()
        {
            this.log.LogInformation("Index Action called.");
            return View();
        }

        [Route("posts/get")]
        [HttpPost]
        public IActionResult GetPosts(GetPostsRequest postsRequest)
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
                        CreatorId = reaction.CreatorId
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
                                CreatorId = reaction.CreatorId
                            })
                        }),
                }).OrderByDescending(p => p.CreationDate)
                .Skip(postsRequest.StartIndex)
                .Take(postsRequest.Count);


            bool loggedIn = int.TryParse(HttpContext.Session.GetString("UserId"), out int activeUserId);

            var model = new IndexModel()
            {
                Posts = postModel,
                MorePostsAvailable = postsRequest.StartIndex + postsRequest.Count < posts.Count()
            };

            return Json(model);
        }

        [Route("posts/add")]
        [HttpPost]
        public IActionResult AddPost(PostModel post)
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
        public IActionResult DeletePost(PostModel post)
        {
            int userTextId = this.posts.GetById(post.PostId).UserTextId;

            try
            {                
                this.comments.RemoveByPostId(post.PostId);
                this.userTexts.Remove(userTextId);
            }
            catch
            {
                return Json(new JSONResponse(false));
            }
            
            return Json(new JSONResponse(true));
        }

        [Route("comments/add")]
        [HttpPost]
        public IActionResult AddComment(CommentModel comment)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var userText = new Data.Models.UserText();

            this.comments.Add(new Data.Models.Comment()
            {
                CreatorId = userId,
                Content = comment.Content,
                UserText = userText,
                Post = this.posts.GetById(comment.PostId),
                PostId = comment.PostId,
                Creator = this.users.GetById(userId),
                CreationDate = DateTime.Now
            });

            return Content("Success :");
        }

        [Route("comments/delete")]
        [HttpPost]
        public IActionResult DeleteComment(CommentModel comment)
        {
            int userTextId = this.comments.GetById(comment.CommentId).UserTextId;

            try
            {
                this.userTexts.Remove(userTextId);
            }
            catch
            {
                return Json(new JSONResponse(false));
            }

            return Json(new JSONResponse(true));
        }

        [Route("reactions/add")]
        [HttpPost]
        public IActionResult AddReaction(ReactionModel reaction)
        {
            // TODO: Session timeout
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var newReaction = (new Data.Models.Reaction()
            {
                Creator = this.users.GetById(userId),
                CreatorId = userId,
                CreationDate = DateTime.Now,
                UserText = this.userTexts.GetById(reaction.TextId),
                UserTextId = reaction.TextId,
                ReactionType = ReactionType.like
            });

            this.reactions.Add(newReaction);
            return Content("Success :");
        }

        [Route("reactions/delete")]
        [HttpPost]
        public IActionResult DeleteReaction(ReactionModel reaction)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            this.reactions.Remove(reaction.TextId, userId);
            
            return Content("Success :");
        }
    }
}
