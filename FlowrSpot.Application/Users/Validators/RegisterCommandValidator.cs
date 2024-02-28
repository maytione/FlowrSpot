using FlowrSpot.Application.Users.Command;
using FluentValidation;


namespace FlowrSpot.Application.Users.Validators
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(p => p.RepeatPassword)
                .Equal(p => p.Password)
                .WithMessage("Passwords do not match");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email address");
        }
    }
}
