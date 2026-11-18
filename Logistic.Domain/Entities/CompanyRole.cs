using Logistic.Domain.Common;
using Logistic.Domain.Enums;

namespace Logistic.Domain.Entities;

public sealed class CompanyRole : BaseEntity
{
    private CompanyRole()
    {
    }

    public Guid CompanyId { get; private set; }

    public CompanyRoleType Role { get; private set; }

    internal static CompanyRole Create(Guid companyId, CompanyRoleType role, DateTime createdDate)
    {
        return new CompanyRole
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            Role = role
        }.WithCreatedAudit(createdDate);
    }

    public static CompanyRole Reconstitute(
        Guid id,
        Guid companyId,
        CompanyRoleType role,
        DateTime createdDate,
        DateTime? modifiedDate,
        Guid? createdById,
        Guid? modifiedById)
    {
        return new CompanyRole
        {
            Id = id,
            CompanyId = companyId,
            Role = role,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            CreatedById = createdById,
            ModifiedById = modifiedById
        };
    }

    private CompanyRole WithCreatedAudit(DateTime createdDate)
    {
        SetAuditOnCreate(createdDate);
        return this;
    }
}
