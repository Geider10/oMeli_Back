using FluentValidation;
using oMeli_Back.DTOs.Store;

namespace oMeli_Back.Validators.Store
{
    public class CreateStoreValidator: AbstractValidator<CreateStoreDto>
    {
        public CreateStoreValidator()
        {
            RuleFor(s => s.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(s => s.SubscriptionId).NotEmpty().WithMessage("SubscriptionId is required");
            RuleFor(s => s.Name).NotEmpty().Length(3, 50).WithMessage("Name is required and must be between 3 and 50 charactters");
            RuleFor(s => s.Wassap).NotEmpty().Length(7,20).WithMessage("Wassap is required and must be between 7 and 20 characters");
            RuleFor(s => s.Mail).NotEmpty().Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Email is required and must be a valid email address.");
            RuleFor(subs => subs.HasLocal).NotEmpty().Must(x => x == true || x == false).WithMessage("State must be 'active' or 'inactive'");
            RuleFor(s => s.Address).Length(0,200).WithMessage("Address must be between 0 and 200 characters");
            RuleFor(s => s.AddressDescription).Length(0, 200).WithMessage("AddressDescription must be between 0 and 200 characters");
            RuleFor(s => s.LocalNumber).Length(0, 20).WithMessage("LocalNumber must be between 0 and 20 characters");
            RuleFor(s => s.CurrentProducts).NotEmpty().WithMessage("CurrectProduc is required");

        }
    }
}
