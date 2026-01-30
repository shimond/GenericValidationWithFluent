using GenericValidationWithFluent.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GenericValidationWithFluent.ExceptionHandlers;

public sealed class ApplicationExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ApplicationExceptionHandler> _logger;

    public ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Only handle our custom base exception type
        if (exception is not BaseApplicationException appException)
            return false; // Pass to next handler

        var traceId = httpContext.TraceIdentifier;
        
        _logger.LogWarning(
            appException,
            "Application exception occurred. TraceId: {TraceId}, ErrorCode: {ErrorCode}, Message: {Message}",
            traceId,
            appException.ErrorCode,
            appException.Message);

        var problemDetails = new ProblemDetails
        {
            Status = appException.StatusCode,
            Title = GetTitle(appException.StatusCode),
            Detail = appException.Message,
            Type = $"https://httpstatuses.com/{appException.StatusCode}",
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["errorCode"] = appException.ErrorCode,
                ["traceId"] = traceId,
                ["timestamp"] = DateTime.UtcNow
            }
        };

        httpContext.Response.StatusCode = appException.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; // Exception handled, stop the chain
    }

    private static string GetTitle(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "Bad Request",
        StatusCodes.Status404NotFound => "Not Found",
        StatusCodes.Status409Conflict => "Conflict",
        _ => "An error occurred"
    };
}
