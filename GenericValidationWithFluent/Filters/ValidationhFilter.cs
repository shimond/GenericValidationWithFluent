using FluentValidation;

namespace GenericValidationWithFluent.Filters;

public sealed class ValidationhFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var dto = context.Arguments
            .FirstOrDefault(a =>
                a is not null &&
                !a.GetType().IsPrimitive &&
                a is not string);

        if (dto is null)
            return await next(context);

        var validatorType = typeof(IValidator<>).MakeGenericType(dto.GetType());

        //read services from the http context (DI)
        var validator = context.HttpContext.RequestServices
            .GetService(validatorType) as IValidator;

        if (validator is null)
            return await next(context);

        // The missing part in the class - we can use ValidationContext with the object to validate
        var validationContext = new ValidationContext<object>(dto);
        var result = await validator.ValidateAsync(validationContext);

        if (!result.IsValid)
        {
            var errors = result.ToDictionary();
            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }
}
