using System;
using System.Collections.Generic;
using System.Linq;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Service
{
    public class PostService : IPost
    {
        private TwikkerContext context;

        public PostService(TwikkerContext context)
        {
            this.context = context;
        }

        public void Add(Post newPost)
        {
            this.context.Add(newPost);
            this.context.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return this.context.Posts;
        }

        public Post GetById(int postId)
        {
            return
                this.context.Posts
                .FirstOrDefault(p => p.PostId == postId);
        }

        public void Remove(int postId)
        {
            this.context.Remove(this.GetById(postId));
            this.context.RemoveRange(this.context.Comments
                .Where(p => p.Post.PostId == postId));            
            this.context.SaveChanges();
        }
    }
}
