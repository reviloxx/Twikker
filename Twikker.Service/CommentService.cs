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

        public IEnumerable<Comment> GetByPostId(int postId)
        {
            return
                this.context.Comments
                .Where(p => p.Post.PostId == postId);
        }

        public Comment GetById(int commentId)
        {
            return
                this.context.Comments
                .FirstOrDefault(c => c.CommentId == commentId);
        }

        public void RemoveByPostId(int postId)
        {
            IEnumerable<Comment> toDelete = this.GetByPostId(postId);            

            for (int i = 0; i < toDelete.Count(); i++)
            {
                int userTextId = toDelete.ElementAt(i).UserTextId;
                this.context.Reactions.RemoveRange(this.context.Reactions.Where(r => r.UserTextId == userTextId));
                this.context.UserText.RemoveRange(this.context.UserText.Where(u => u.UserTextId == userTextId));
            }
            
            this.context.SaveChanges();
        }

        public void Remove(int commentId)
        {
            this.context.Remove(this.GetById(commentId));
            this.context.SaveChanges();
        }
    }
}
