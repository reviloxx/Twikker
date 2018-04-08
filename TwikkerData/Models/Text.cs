using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Twikker.Data.Models
{
    public abstract class Text
    {
        public int TextId { get; set; }

        [Required]
        public virtual int UserId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public virtual IEnumerable<Reaction> Reactions { get; set; }
    }
}
