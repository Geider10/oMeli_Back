using FluentValidation;
using oMeli_Back.DTOs.ProductCategory;

namespace oMeli_Back.Validators.ProductCategory;

public class CreateProductCategoryDtoValidator : AbstractValidator<CreateProductCategoryDto>
{
    public CreateProductCategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100);

        RuleFor(x => x.Description).NotEmpty().MinimumLength(2).MaximumLength(256);
    }
}
