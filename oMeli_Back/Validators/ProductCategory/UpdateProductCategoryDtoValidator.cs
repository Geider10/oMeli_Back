using FluentValidation;
using oMeli_Back.DTOs.ProductCategory;

namespace oMeli_Back.Validators.ProductCategory;

public class UpdateProductCategoryDtoValidator : AbstractValidator<UpdateProductCategoryDto>
{
    public UpdateProductCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(2)
            .MaximumLength(100)
            .When(x => x.Name != null);

        RuleFor(x => x.Description)
            .MinimumLength(2)
            .MaximumLength(100)
            .When(x => x.Description != null);
    }
}

