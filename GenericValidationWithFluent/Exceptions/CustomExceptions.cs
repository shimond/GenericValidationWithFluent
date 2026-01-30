namespace GenericValidationWithFluent.Exceptions;

public sealed class ProductNotFoundException : BaseApplicationException
{
    public ProductNotFoundException(int productId)
        : base(
            message: $"Product with ID {productId} was not found.",
            statusCode: StatusCodes.Status404NotFound,
            errorCode: "PRODUCT_NOT_FOUND")
    {
    }
}

public sealed class ProductAlreadyExistsException : BaseApplicationException
{
    public ProductAlreadyExistsException(string productName)
        : base(
            message: $"Product '{productName}' already exists.",
            statusCode: StatusCodes.Status409Conflict,
            errorCode: "PRODUCT_ALREADY_EXISTS")
    {
    }
}

public sealed class InsufficientStockException : BaseApplicationException
{
    public InsufficientStockException(int available, int requested)
        : base(
            message: $"Insufficient stock. Available: {available}, Requested: {requested}.",
            statusCode: StatusCodes.Status400BadRequest,
            errorCode: "INSUFFICIENT_STOCK")
    {
    }
}
