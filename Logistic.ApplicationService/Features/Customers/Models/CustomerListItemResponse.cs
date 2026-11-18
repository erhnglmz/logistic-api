namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class CustomerListItemResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string CompanyType { get; init; } = string.Empty;

    public string? TaxNumber { get; init; }

    public string? NationalId { get; init; }

    public string? Phone { get; init; }

    public string? Email { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedDate { get; init; }
}
