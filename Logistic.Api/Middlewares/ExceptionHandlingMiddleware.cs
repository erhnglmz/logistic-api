using System.Net;
using System.Text.Json;
using Logistic.Api.Models;
using Logistic.Domain.Exceptions;

namespace Logistic.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

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
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, code, message, errors) = exception switch
        {
            ValidationException validationException => (
                HttpStatusCode.BadRequest,
                "validation_error",
                validationException.Message,
                validationException.Errors),
            EntityNotFoundException notFoundException => (
                HttpStatusCode.NotFound,
                "not_found",
                notFoundException.Message,
                null),
            ConflictException conflictException => (
                HttpStatusCode.Conflict,
                "conflict",
                conflictException.Message,
                null),
            BusinessRuleException businessRuleException => (
                HttpStatusCode.Conflict,
                "business_rule_violation",
                businessRuleException.Message,
                null),
            _ => (
                HttpStatusCode.InternalServerError,
                "internal_error",
                "An unexpected error occurred.",
                null)
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception");
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ApiErrorResponse
        {
            Code = code,
            Message = message,
            Errors = errors
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }
}
