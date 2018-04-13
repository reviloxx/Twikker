using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Twikker.Data.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public string Content { get; set; }

        public virtual IEnumerable<Reaction> Reactions { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }        
    }
}
