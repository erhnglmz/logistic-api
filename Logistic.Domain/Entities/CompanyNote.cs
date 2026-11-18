using Logistic.Domain.Common;
using Logistic.Domain.Exceptions;

namespace Logistic.Domain.Entities;

public sealed class CompanyNote : BaseEntity
{
    private CompanyNote()
    {
    }

    public Guid CompanyId { get; private set; }

    public string NoteText { get; private set; } = string.Empty;

    public DateOnly? ContactDate { get; private set; }

    internal static CompanyNote Create(
        Guid companyId,
        string noteText,
        DateOnly? contactDate,
        DateTime createdDate)
    {
        if (string.IsNullOrWhiteSpace(noteText))
        {
            throw new ValidationException("Note text is required.");
        }

        return new CompanyNote
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            NoteText = noteText.Trim(),
            ContactDate = contactDate
        }.WithCreatedAudit(createdDate);
    }

    public static CompanyNote Reconstitute(
        Guid id,
        Guid companyId,
        string noteText,
        DateOnly? contactDate,
        DateTime createdDate,
        DateTime? modifiedDate,
        Guid? createdById,
        Guid? modifiedById)
    {
        return new CompanyNote
        {
            Id = id,
            CompanyId = companyId,
            NoteText = noteText,
            ContactDate = contactDate,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            CreatedById = createdById,
            ModifiedById = modifiedById
        };
    }

    private CompanyNote WithCreatedAudit(DateTime createdDate)
    {
        SetAuditOnCreate(createdDate);
        return this;
    }
}
