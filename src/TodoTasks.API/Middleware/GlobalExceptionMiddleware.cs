using FluentValidation;
using System.Net;
using System.Text.Json;
using TodoTasks.Application.Common.Exceptions;

namespace TodoTasks.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode;
        string message;
        object? errors;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                message = "Validation failed";
                errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToList();
                break;
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                errors = null;
                break;
            case InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                errors = null;
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
                errors = null;
                break;
            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = "Resource not found";
                errors = null;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "An error occurred while processing your request";
                errors = null;
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = errors != null
            ? (object)new { error = message, statusCode = (int)statusCode, errors }
            : (object)new { error = message, statusCode = (int)statusCode };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}