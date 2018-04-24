using System;
using System.Collections.Generic;
using System.Text;
using Twikker.Data.Models;

namespace Twikker.Data
{
    public interface IComment
    {
        IEnumerable<Comment> GetByPostId(int postId);       

        void Add(Comment newComment);
        Comment GetById(int commentId);
        void RemoveByPostId(int postId);
        void Remove(int commentId);        
    }
}
