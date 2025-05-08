using FluentValidation;
using oMeli_Back.DTOs.Auth;

namespace oMeli_Back.Validators.Auth
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(p => p.CurrentPassword).NotEmpty();
            RuleFor(p => p.NewPassword).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$").WithMessage("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número.");
        }
    }
}
