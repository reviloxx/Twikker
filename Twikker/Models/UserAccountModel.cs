using System;
using System.ComponentModel.DataAnnotations;

namespace Twikker.Web.Models
{
    public class UserAccountModel
    {
        public UserAccountModel()
        {
            this.UserId = -1;
            this.LastName = string.Empty;
        }

        public int UserId { get; set; }

        [Required(ErrorMessage ="Nickname is required.")]
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }        
    }
}
