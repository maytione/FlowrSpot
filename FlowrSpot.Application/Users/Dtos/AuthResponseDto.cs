
namespace FlowrSpot.Application.Users.Dtos
{
    public class AuthResponseDto
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }
        public string? AccessToken { get; set; }
    }
}
