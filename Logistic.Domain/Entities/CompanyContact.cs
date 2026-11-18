using Logistic.Domain.Common;
using Logistic.Domain.Exceptions;

namespace Logistic.Domain.Entities;

public sealed class CompanyContact : BaseEntity
{
    private CompanyContact()
    {
    }

    public Guid CompanyId { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string? Title { get; private set; }

    public string? Phone { get; private set; }

    public string? Email { get; private set; }

    public bool IsPrimary { get; private set; }

    internal static CompanyContact Create(
        Guid companyId,
        string firstName,
        string lastName,
        string? title,
        string? phone,
        string? email,
        bool isPrimary,
        DateTime createdDate)
    {
        ValidateName(firstName, lastName);

        return new CompanyContact
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Title = title?.Trim(),
            Phone = phone?.Trim(),
            Email = email?.Trim(),
            IsPrimary = isPrimary
        }.WithCreatedAudit(createdDate);
    }

    public static CompanyContact Reconstitute(
        Guid id,
        Guid companyId,
        string firstName,
        string lastName,
        string? title,
        string? phone,
        string? email,
        bool isPrimary,
        DateTime createdDate,
        DateTime? modifiedDate,
        Guid? createdById,
        Guid? modifiedById)
    {
        return new CompanyContact
        {
            Id = id,
            CompanyId = companyId,
            FirstName = firstName,
            LastName = lastName,
            Title = title,
            Phone = phone,
            Email = email,
            IsPrimary = isPrimary,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            CreatedById = createdById,
            ModifiedById = modifiedById
        };
    }

    internal void MarkAsPrimary(DateTime modifiedDate)
    {
        IsPrimary = true;
        SetAuditOnUpdate(modifiedDate);
    }

    internal void ClearPrimary(DateTime modifiedDate)
    {
        IsPrimary = false;
        SetAuditOnUpdate(modifiedDate);
    }

    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ValidationException("Contact first name is required.");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ValidationException("Contact last name is required.");
        }
    }

    private CompanyContact WithCreatedAudit(DateTime createdDate)
    {
        SetAuditOnCreate(createdDate);
        return this;
    }
}
