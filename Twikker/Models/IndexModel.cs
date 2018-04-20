using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twikker.Data.Models;

namespace Twikker.Web.Models
{
    public class IndexModel
    {
        public int activeUserId { get; set; }

        public IEnumerable<PostModel> Posts { get; set; }        
    }

    public class PostModel
    {
        public int PostId { get; set; }

        public int UserTextId { get; set; }

        public int CreatorId { get; set; }

        public string CreatorNickname { get; set; }

        public string CreationDate { get; set; }

        public string Content { get; set; }

        public IEnumerable<ReactionModel> Reactions { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; }        
    }

    public class CommentModel
    {
        public int CommentId { get; set; }

        public int UserTextId { get; set; }

        public int CreatorId { get; set; }

        public string CreatorNickname { get; set; }

        public string CreationDate { get; set; }

        public string Content { get; set; }        

        public IEnumerable<ReactionModel> Reactions { get; set; }
    }

    public class ReactionModel
    {
        public ReactionType Reaction { get; set; }
    }
}
