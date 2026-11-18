using Logistic.Domain.Common;
using Logistic.Domain.Exceptions;

namespace Logistic.Domain.Entities;

public sealed class CompanyBankAccount : BaseEntity
{
    private CompanyBankAccount()
    {
    }

    public Guid CompanyId { get; private set; }

    public string Iban { get; private set; } = string.Empty;

    public string? BankName { get; private set; }

    public bool IsDefault { get; private set; }

    internal static CompanyBankAccount Create(
        Guid companyId,
        string iban,
        string? bankName,
        bool isDefault,
        DateTime createdDate)
    {
        if (string.IsNullOrWhiteSpace(iban))
        {
            throw new ValidationException("IBAN is required.");
        }

        return new CompanyBankAccount
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            Iban = iban.Trim().ToUpperInvariant(),
            BankName = bankName?.Trim(),
            IsDefault = isDefault
        }.WithCreatedAudit(createdDate);
    }

    public static CompanyBankAccount Reconstitute(
        Guid id,
        Guid companyId,
        string iban,
        string? bankName,
        bool isDefault,
        DateTime createdDate,
        DateTime? modifiedDate,
        Guid? createdById,
        Guid? modifiedById)
    {
        return new CompanyBankAccount
        {
            Id = id,
            CompanyId = companyId,
            Iban = iban,
            BankName = bankName,
            IsDefault = isDefault,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            CreatedById = createdById,
            ModifiedById = modifiedById
        };
    }

    internal void MarkAsDefault(DateTime modifiedDate)
    {
        IsDefault = true;
        SetAuditOnUpdate(modifiedDate);
    }

    internal void ClearDefault(DateTime modifiedDate)
    {
        IsDefault = false;
        SetAuditOnUpdate(modifiedDate);
    }

    private CompanyBankAccount WithCreatedAudit(DateTime createdDate)
    {
        SetAuditOnCreate(createdDate);
        return this;
    }
}
