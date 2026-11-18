namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class AddCustomerContactRequest
{
    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string? Title { get; init; }

    public string? Phone { get; init; }

    public string? Email { get; init; }

    public bool IsPrimary { get; init; }
}
