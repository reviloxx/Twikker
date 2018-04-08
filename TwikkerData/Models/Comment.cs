using System;
using System.Collections.Generic;
using System.Text;

namespace Twikker.Data.Models
{
    public class Comment : Text
    {
        public virtual Post Post { get; set; }
    }
}
