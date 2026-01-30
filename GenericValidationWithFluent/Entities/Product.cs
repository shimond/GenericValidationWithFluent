namespace GenericValidationWithFluent.Entities;

/// <summary>
/// Domain entity representing a product in the database
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation property for categories (example of relationship)
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}

/// <summary>
/// Category entity for demonstration purposes
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
