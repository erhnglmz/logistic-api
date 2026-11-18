namespace Logistic.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    public DateTime CreatedDate { get; protected set; }

    public DateTime? ModifiedDate { get; protected set; }

    public Guid? CreatedById { get; protected set; }

    public Guid? ModifiedById { get; protected set; }

    protected void SetAuditOnCreate(DateTime createdDate, Guid? createdById = null)
    {
        CreatedDate = createdDate;
        CreatedById = createdById;
    }

    protected void SetAuditOnUpdate(DateTime modifiedDate, Guid? modifiedById = null)
    {
        ModifiedDate = modifiedDate;
        ModifiedById = modifiedById;
    }
}
