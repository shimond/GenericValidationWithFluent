using FluentValidation;
using GenericValidationWithFluent.DTOs;

namespace GenericValidationWithFluent.Validators;

public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
{
    public CreateProductDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);
    }
}
