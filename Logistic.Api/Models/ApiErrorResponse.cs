namespace Logistic.Api.Models;

public sealed class ApiErrorResponse
{
    public required string Code { get; init; }

    public required string Message { get; init; }

    public IReadOnlyDictionary<string, string[]>? Errors { get; init; }
}
