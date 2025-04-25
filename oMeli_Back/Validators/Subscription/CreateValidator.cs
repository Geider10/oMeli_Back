using FluentValidation;
using oMeli_Back.DTOs.Subscription;

namespace oMeli_Back.Validators.Subscription
{
    public class CreateValidator: AbstractValidator<CreateDto>
    {
        public CreateValidator()
        {
            RuleFor(subs => subs.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(subs => subs.PlanId).NotEmpty().WithMessage("PlanId is required");
            RuleFor(subs => subs.State).NotEmpty().Must(x => x == "active" || x == "inactive").WithMessage("State must be 'active' or 'inactive'");
        }
    }
}
