using FluentValidation;
using oMeli_Back.DTOs.Subscription;

namespace oMeli_Back.Validators.Subscription
{
    public class UpdateValidator: AbstractValidator<UpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(subs => subs.PlanId).NotEmpty().WithMessage("PlanId is required");
            RuleFor(subs => subs.State).NotEmpty().Must(x => x == "active" || x == "inactive").WithMessage("State must be 'active' or 'inactive'");
            RuleFor(subs => subs.DataStart).NotEmpty().Matches(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$").WithMessage("DateStart must be in the format dd/MM/yyy");
            RuleFor(subs => subs.DateEnd).NotEmpty().Matches(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$").WithMessage("DateEnd must be in the format dd/MM/yyy");
        }
    }
}
