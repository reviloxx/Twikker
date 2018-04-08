using System;
using System.Collections.Generic;
using System.Text;
using Twikker.Data.Models;

namespace Twikker.Data
{
    public interface IPost
    {
        IEnumerable<Post> GetAll();

        void Add(Post newPost);
        Post GetById(int postId);        
        void Remove(int postId);
    }
}
