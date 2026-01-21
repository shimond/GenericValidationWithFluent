using GenericValidationWithFluent.DTOs;
using GenericValidationWithFluent.Filters;

namespace GenericValidationWithFluent.Routes;

public static class ProductsRoutes
{
    public static void MapProductsRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/products").AddEndpointFilter<ValidationhFilter>();

        group.MapPost("/", CreateProduct);
        group.MapPut("/", UpdateProduct);
    }

    private static IResult CreateProduct(CreateProductDTO dto)
    {
        return Results.Ok(new { Message = "Product created successfully", Data = dto });
    }

    private static IResult UpdateProduct(UpdateProductDTO dto)
    {
        return Results.Ok(new { Message = "Product updated successfully", Data = dto });
    }
}
