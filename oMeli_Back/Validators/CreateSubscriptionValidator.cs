using FluentValidation;
using oMeli_Back.DTOs;

namespace oMeli_Back.Validators
{
    public class CreateSubscriptionValidator: AbstractValidator<CreateSubscriptionDto>
    {
        public CreateSubscriptionValidator()
        {
            RuleFor(subs => subs.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(subs => subs.PlanId).NotEmpty().WithMessage("PlanId is required");
        }
    }
}
