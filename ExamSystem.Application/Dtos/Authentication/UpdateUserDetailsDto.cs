using System.ComponentModel.DataAnnotations;

namespace ExamSystem.Application.Dtos.Authentication
{
    public class UpdateUserDetailsDto
    {
        [Required]
        public string Id { get; set; }
        
        public string? FirstName {  get; set; }
        
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
