namespace Logistic.ApplicationService.Features.Customers.Models;

public sealed class CustomerResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string CompanyType { get; init; } = string.Empty;

    public string? TaxOffice { get; init; }

    public string? TaxNumber { get; init; }

    public string? NationalId { get; init; }

    public string? Address { get; init; }

    public string? Phone { get; init; }

    public string? Email { get; init; }

    public string? AccountingContactInfo { get; init; }

    public int? DefaultPaymentTermDays { get; init; }

    public decimal? DefaultVatRate { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedDate { get; init; }

    public DateTime? ModifiedDate { get; init; }

    public IReadOnlyList<string> Roles { get; init; } = [];

    public IReadOnlyList<CustomerContactResponse> Contacts { get; init; } = [];

    public IReadOnlyList<CustomerNoteResponse> Notes { get; init; } = [];

    public IReadOnlyList<CustomerBankAccountResponse> BankAccounts { get; init; } = [];
}
