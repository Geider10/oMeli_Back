using FluentValidation;
using oMeli_Back.DTOs.Store;

namespace oMeli_Back.Validators.Store
{
    public class CreatePaymentMethodValidator: AbstractValidator<CreatePaymentMethodDto>
    {
        public CreatePaymentMethodValidator()
        {
            RuleFor(pm => pm.StoreId).NotEmpty().WithMessage("StoreId is required");
            RuleFor(pm => pm.Name).NotEmpty().Length(3, 50).WithMessage("Name is required and must be between 3 and 50 characters");
            RuleFor(pm => pm.Type).NotEmpty().Must(pm => pm == "Efectivo" || pm == "Transferencia" || pm == "Tarjeta").WithMessage("Type is required and must be Efectivo, Transferencia or Tarjeta");
        }
    }
}
