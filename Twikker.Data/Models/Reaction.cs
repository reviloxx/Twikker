﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Twikker.Data.Models
{
    public class Reaction
    {
        public int ReactionId { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required]
        public int UserTextId { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public ReactionType ReactionType { get; set; }

        [Required]
        public virtual UserText UserText { get; set; }
    }

    public enum ReactionType
    {
        like
    }
}
