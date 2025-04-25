using FluentValidation;
using oMeli_Back.DTOs.Subscription;

namespace oMeli_Back.Validators.Subscription
{
    public class UpdateValidator: AbstractValidator<UpdateDto>
    {
        public UpdateValidator()
        {
            RuleFor(subs => subs.SubscriptionId).NotEmpty().WithMessage("SubscriptionId is required");
            RuleFor(subs => subs.PlanId).NotEmpty().WithMessage("PlanId is required");
            RuleFor(subs => subs.State).NotEmpty().Must(x => x == "active" || x == "inactive").WithMessage("State must be 'active' or 'inactive'");
        }
    }
}
