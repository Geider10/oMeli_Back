using FluentValidation;
using oMeli_Back.DTOs.Auth;
namespace oMeli_Back.Validators.Auth
{
    public class SignUpValidator : AbstractValidator<SignUpDto>
    {
        public SignUpValidator()
        {
            RuleFor( user => user.Name).NotNull().Length(3,50).WithMessage("Name is required and must be between 3 and 50 characters.");
            RuleFor( user => user.LastName).NotNull().Length(3,50).WithMessage("Last name is required and must be between 3 and 50 characters.");
            RuleFor( user => user.Phone).NotNull().Length(7,20).WithMessage("Phone is required and must be between 7 and 20 characters.");
            RuleFor( user => user.Email).NotNull().Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Email is required and must be a valid email address.");
            RuleFor( user => user.Password).NotNull().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número.");
        }
    }
}
