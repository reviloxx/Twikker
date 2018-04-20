﻿using System;
using System.Collections.Generic;
using System.Text;
using Twikker.Data.Models;

namespace Twikker.Data
{
    public interface IReaction
    {
        void Add(Reaction reaction);
        IEnumerable<Reaction> GetAll(int userTextId);
        Reaction GetById(int reactionId);
        void RemoveByTextId(int textId);
        void Remove(int reactionId);
    }
}
