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
}
