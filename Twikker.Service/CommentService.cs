using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Service
{
    public class CommentService : IComment
    {
        private TwikkerContext context;

        public CommentService(TwikkerContext context)
        {
            this.context = context;
        }

        public void Add(Comment newComment)
        {
            this.context.Add(newComment);
            this.context.SaveChanges();
        }

        public IEnumerable<Comment> GetAll(int postId)
        {
            return 
                this.context.Posts
                .FirstOrDefault(p => p.PostId == postId)
                .Comments;
        }

        public Comment GetById(int commentId)
        {
            return
                this.context.Comments
                .FirstOrDefault(c => c.CommentId == commentId);
        }

        public void Remove(int commentId)
        {
            this.context.Remove(this.GetById(commentId));
        }
    }
}
