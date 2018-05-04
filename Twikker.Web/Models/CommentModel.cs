using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twikker.Web.Models
{
    //public class CommentModel
    //{
    //    public int CommentId { get; set; }

       

    //    public string Content { get; set; }        
    //}

    public class CommentModel
    {
        public int CommentId { get; set; }

        public int UserTextId { get; set; }

        public int PostId { get; set; }

        public int CreatorId { get; set; }

        public string CreatorNickname { get; set; }

        public string CreationDate { get; set; }

        public string Content { get; set; }

        public IEnumerable<ReactionModel> Reactions { get; set; }
    }
}
