using System;
using System.Collections.Generic;
using System.Text;

namespace Twikker.Data.Models
{
    public class Post : Text
    {
        public virtual IEnumerable<Comment> Comments { get; set; }        
    }
}
