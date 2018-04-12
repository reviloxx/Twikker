using System;
using System.Collections.Generic;
using System.Text;
using Twikker.Data.Models;

namespace Twikker.Data
{
    public interface IReaction
    {
        void Add(Reaction reaction);
        IEnumerable<Reaction> GetAll(Post post, int postId);
        IEnumerable<Reaction> GetAll(Comment comment, int commentId);
        Reaction GetById(int reactionId);
        void Remove(int reactionId);
    }
}
