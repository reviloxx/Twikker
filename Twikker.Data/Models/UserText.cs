using System;
using System.Collections.Generic;
using System.Text;

namespace Twikker.Data.Models
{
    public class UserText
    {
        public int UserTextId { get; set; }

        public virtual IEnumerable<Reaction> Reactions { get; set; }
    }
}
