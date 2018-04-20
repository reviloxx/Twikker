using System;
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

        public IEnumerable<Reaction> GetAll(int userTextId)
        {
            var reactions = this.context.Reactions
                .Where(r => r.UserText.UserTextId == userTextId);
            return reactions;
                
        }

        public Reaction GetById(int reactionId)
        {
            return
                this.context.Reactions
                .FirstOrDefault(r => r.ReactionId == reactionId);
        }

        public void Remove(int textId, int activeUserId)
        {
            this.context.RemoveRange(this.context.Reactions
                .Where(r => r.UserText.UserTextId == textId && r.Creator.UserId == activeUserId));
            this.context.SaveChanges();
        }

        public void Remove(int textId)
        {
            this.context.RemoveRange(this.context.Reactions
                .Where(r => r.UserText.UserTextId == textId));
            this.context.SaveChanges();
        }

        //public void RemoveById(int reactionId)
        //{
        //    this.context.Remove(this.GetById(reactionId));
        //    this.context.SaveChanges();
        //}
    }
}
