namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class AddCustomerBankAccountRequest
{
    public string Iban { get; init; } = string.Empty;

    public string? BankName { get; init; }

    public bool IsDefault { get; init; }
}
