using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twikker.Web.Models
{
    public class AddCommentModel
    {
        public string Content { get; set; }

        public int PostId { get; set; }
    }
}
