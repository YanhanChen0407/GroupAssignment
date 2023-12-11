using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static GroupAssignment.Data.Enums;

namespace GroupAssignment.Models
{
    public class UserModel : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        public string Role { get; set; }

        [Required(ErrorMessage = "User name cannot be empty")]
        [UniqueUsername(ErrorMessage = "Username already exists")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService(typeof(AppDbContext)) as AppDbContext;

            var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == Username && u.Id != Id);
            if (existingUser != null)
            {
                yield return new ValidationResult("Username already exists", new[] { nameof(Username) });
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService(typeof(AppDbContext)) as AppDbContext;

            var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == value.ToString());
            if (existingUser != null)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
