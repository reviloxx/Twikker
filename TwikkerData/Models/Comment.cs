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
        public int CreatorId { get; set; }

        [Required]
        public int UserTextId { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        [Required]
        public virtual UserText UserText { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string Content { get; set; }        

        [Required]
        public virtual Post Post { get; set; }
        
        //[Required]
        //public int PostId { get; set; }
    }
}
