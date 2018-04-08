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
    }

    public class PostModel
    {
        public string Text { get; set; }

        public int TextId { get; set; }

        public DateTime CreationDate { get; set; }

        public string Creator { get; set; }

        public int CreatorId { get; set; }
    }
}
