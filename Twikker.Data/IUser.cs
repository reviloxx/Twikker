using System;
using System.Collections.Generic;
using System.Text;
using Twikker.Data.Models;

namespace Twikker.Data
{
    public interface IUser
    {
        IEnumerable<User> GetAll();
        void Add(User newUser);
        User GetById(int userId);
        User GetByNickname(string nickname);
        User GetByEmail(string email);
        void Update(User updatedUser);
        void Remove(int userId);
    }
}
