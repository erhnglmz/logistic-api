namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class CustomerContactResponse
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string? Title { get; init; }

    public string? Phone { get; init; }

    public string? Email { get; init; }

    public bool IsPrimary { get; init; }
}
