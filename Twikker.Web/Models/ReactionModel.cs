using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twikker.Data.Models;

namespace Twikker.Web.Models
{
    public class ReactionModel
    {      
        public int CreatorId { get; set; }

        public int TextId { get; set; }

        public ReactionType Reaction { get; set; }
    }
}
