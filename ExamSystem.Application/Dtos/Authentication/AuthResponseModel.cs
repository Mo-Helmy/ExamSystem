#nullable disable

using ExamSystem.Application.Dtos.Authentication;

namespace ExamSystem.Application.Dtos.Authentication
{
    public class AuthResponseModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
