namespace GenericValidationWithFluent.DTOs;

public record CreateProductDTO
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}
