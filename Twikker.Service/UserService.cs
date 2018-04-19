using System;
using System.Collections.Generic;
using System.Linq;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Service
{
    public class UserService : IUser
    {
        private TwikkerContext context;

        public UserService(TwikkerContext context)
        {
            this.context = context;
        }

        public void Add(User newUser)
        {
            this.context.Add(newUser);
            this.context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users;
        }
                
        public User GetById(int userId)
        {
            return this.GetAll()
                .FirstOrDefault(u => u.UserId == userId);
        }

        public User GetByEmail(string email)
        {
            return this.GetAll()
                .FirstOrDefault(u => u.Email == email);
        }

        public User GetByNickname(string nickname)
        {
            return this.GetAll()
                .FirstOrDefault(u => u.NickName == nickname);
        }

        public void Update(User updatedUser)
        {
            var user = this.GetById(updatedUser.UserId);
            user.NickName = updatedUser.NickName;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.DateOfBirth = updatedUser.DateOfBirth;
            //updatedUser.Password = this.GetById(updatedUser.UserId).Password;
            //this.Remove(updatedUser.UserId);
            //this.Add(updatedUser);
            this.context.SaveChanges();
        }

        public void Remove(int userId)
        {
            this.context.Remove(this.GetById(userId));
            this.context.SaveChanges();
        }
    }
}
