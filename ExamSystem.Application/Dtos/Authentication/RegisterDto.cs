#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ExamSystem.Application.Dtos.Authentication
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "UserTypeId is required")]
        public int UserTypeId { get; set; }
    }
}
