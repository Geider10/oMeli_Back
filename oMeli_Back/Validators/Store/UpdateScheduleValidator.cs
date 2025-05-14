using FluentValidation;
using oMeli_Back.DTOs.Store;
namespace oMeli_Back.Validators.Store
{
    public class UpdateScheduleValidator : AbstractValidator<UpdateScheduleDto>
    {
        public UpdateScheduleValidator()
        {
            RuleFor(s => s.Day).Must(x => x == "Lunes" || x == "Martes" || x == "Miércoles" || x == "Jueves" || x == "Viernes" || x == "Sábado" || x == "Domingo")
            .WithMessage("Day must be 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado' or 'Domingo'");
            RuleFor(s => s.HourStart).NotEmpty().WithMessage("HourStart is required");
            RuleFor(s => s.HourEnd).NotEmpty().WithMessage("HourEnd is required");
        }
    }
}
