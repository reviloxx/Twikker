﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Service
{
    public class ReactionService : IReaction
    {
        private TwikkerContext context;

        public ReactionService(TwikkerContext context)
        {
            this.context = context;
        }

        public void Add(Reaction reaction)
        {
            this.context.Add(reaction);
            this.context.SaveChanges();
        }

        public IEnumerable<Reaction> GetAll(Post post, int postId)
        {
            return
                this.context.Posts
                .FirstOrDefault(pos => pos.PostId == postId)?
                .Reactions;
        }

        public IEnumerable<Reaction> GetAll(Comment comment, int commentId)
        {
            return
                this.context.Comments
                .FirstOrDefault(com => com.CommentId == commentId)?
                .Reactions;
        }

        public Reaction GetById(int reactionId)
        {
            return
                this.context.Reactions
                .FirstOrDefault(r => r.ReactionId == reactionId);
        }

        public void Remove(int reactionId)
        {
            this.context.Remove(this.GetById(reactionId));
        }
    }
}
