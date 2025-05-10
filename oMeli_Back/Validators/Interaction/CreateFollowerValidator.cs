using FluentValidation;
using oMeli_Back.DTOs.Interaction;

namespace oMeli_Back.Validators.Interaction
{
    public class CreateFollowerValidator : AbstractValidator<CreateFollowerDto>
    {
        public CreateFollowerValidator()
        {
            RuleFor(f => f.StoreId).NotEmpty().WithMessage("StoreId is required");
            RuleFor(f => f.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}
