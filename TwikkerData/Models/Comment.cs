using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Twikker.Data.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public virtual Post Post { get; set; }

        public virtual IEnumerable<Reaction> Reactions { get; set; }
    }
}
