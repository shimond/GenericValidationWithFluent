using FluentValidation;
using GenericValidationWithFluent.DTOs;

namespace GenericValidationWithFluent.Validators;

public class UpdateProductDTOValidator : AbstractValidator<UpdateProductDTO>
{
    public UpdateProductDTOValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

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
