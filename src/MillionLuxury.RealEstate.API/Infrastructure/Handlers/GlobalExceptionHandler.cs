using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MillionLuxury.RealEstate.Domain.Exceptions.Common;

namespace MillionLuxury.RealEstate.API.Infrastructure.Handlers;
internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> _logger
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var (statusCode, title) = GetExceptionDetails(exception);

        var problemDetails = new ProblemDetails {
            Status = statusCode,
            Title = title,
            Detail = exception.Message
        };

        httpContext.Features.Set(new ProblemDetailsContext {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception,
        });

        httpContext.Response.StatusCode = statusCode;

        var problemDetailsService = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
        await problemDetailsService.WriteAsync(new() {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });

        return true;
    }

    private static (int StatusCode, string Title) GetExceptionDetails(Exception exception)
    {
        if (exception is BaseException baseException)
            return ((int)baseException.StatusCode, baseException.ErrorCode);

        return exception switch {
            ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };
    }
}
