namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class CustomerBankAccountResponse
{
    public Guid Id { get; init; }

    public string Iban { get; init; } = string.Empty;

    public string? BankName { get; init; }

    public bool IsDefault { get; init; }
}
