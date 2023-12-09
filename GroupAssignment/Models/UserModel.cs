using System;
using System.ComponentModel.DataAnnotations;

namespace GroupAssignment.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User name cannot be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
