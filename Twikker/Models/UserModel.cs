using System;
using System.ComponentModel.DataAnnotations;

namespace Twikker.Web.Models
{
    public class UserModel
    {
        public UserModel()
        {
            this.UserId = -1;
            this.LastName = string.Empty;
        }

        public int UserId { get; set; }
        
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }        
    }
}
