namespace GenericValidationWithFluent.Exceptions;

public abstract class BaseApplicationException : Exception
{
    public int StatusCode { get; init; }
    public string ErrorCode { get; init; }

    protected BaseApplicationException(
        string message,
        int statusCode,
        string errorCode,
        Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
