using System.Net;

namespace EventsTP.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Unauthorized access.");
            await HandleExceptionAsync(context, "Unauthorized access.", StatusCodes.Status401Unauthorized);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Resource not found.");
            await HandleExceptionAsync(context, "Resource not found.", StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, "An unexpected error occurred. Please try again later.", StatusCodes.Status500InternalServerError);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}