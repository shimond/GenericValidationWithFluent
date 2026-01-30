namespace GenericValidationWithFluent.DTOs;

/// <summary>
/// Response DTO for product details
/// </summary>
public record ProductResponseDTO
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public bool IsActive { get; init; }
    
    // Nested DTO for category
    public CategoryResponseDTO? Category { get; init; }
}

/// <summary>
/// Response DTO for category
/// </summary>
public record CategoryResponseDTO
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

/// <summary>
/// Lightweight DTO for product lists
/// </summary>
public record ProductListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public bool IsActive { get; init; }
}
