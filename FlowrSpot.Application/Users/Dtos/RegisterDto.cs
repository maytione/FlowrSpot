﻿
namespace FlowrSpot.Application.Users.Dtos
{
    public class RegisterDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string RepeatPassword { get; set; }
    }
}
