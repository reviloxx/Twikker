using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twikker.Data.Models;

namespace Twikker.Web.Models
{
    public class IndexModel
    {
        public IEnumerable<PostModel> Posts { get; set; }

        public int activeUserId { get; set; }
    }

    public class PostModel
    {
        public string Content { get; set; }

        public int PostId { get; set; }

        public string CreationDate { get; set; }

        public string CreatorNickname { get; set; }

        public int CreatorId { get; set; }
    }
}
