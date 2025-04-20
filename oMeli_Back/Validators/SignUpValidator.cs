using FluentValidation;
using oMeli_Back.DTOs;
namespace oMeli_Back.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpDto>
    {
        public SignUpValidator()
        {
            RuleFor( user => user.Name).NotNull().Length(3,50).WithMessage("Name is required and must be between 3 and 50 characters.");
            RuleFor( user => user.LastName).NotNull().Length(3,50).WithMessage("Last name is required and must be between 3 and 50 characters.");
            RuleFor( user => user.Phone).NotNull().Length(7,20).WithMessage("Phone is required and must be between 7 and 20 characters.");
            RuleFor( user => user.Email).NotNull().EmailAddress().WithMessage("Email is required and must be a valid email address.");
            RuleFor( user => user.Password).NotNull().Length(8,50).WithMessage("Password is required and must be between 8 and 50 characters.");
        }
    }
}
