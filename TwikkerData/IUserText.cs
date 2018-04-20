using System;
using System.Collections.Generic;
using System.Text;
using Twikker.Data.Models;

namespace Twikker.Data
{
    public interface IUserText
    {
        UserText GetById(int userTextId);
    }
}
