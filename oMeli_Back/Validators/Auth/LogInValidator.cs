using FluentValidation;
using oMeli_Back.DTOs.Auth;

namespace oMeli_Back.Validators.Auth
{
    public class LogInValidator : AbstractValidator<LogInDto>
    {
        public LogInValidator()
        {
            RuleFor(user => user.Email).NotNull().WithMessage("Email is required and must be a valid email address.");
            RuleFor(user => user.Password).NotNull().WithMessage("Password is required and must be lowly 8 characters.");
        }
    }
}
