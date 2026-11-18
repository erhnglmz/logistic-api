namespace Logistic.Domain.Exceptions;

public sealed class ValidationException : DomainException
{
    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors)
        : base(message)
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]>? Errors { get; }
}
