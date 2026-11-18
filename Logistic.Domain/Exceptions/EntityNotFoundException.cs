namespace Logistic.Domain.Exceptions;

public sealed class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, Guid id)
        : base($"{entityName} with id '{id}' was not found.")
    {
        EntityName = entityName;
        EntityId = id;
    }

    public string EntityName { get; }

    public Guid EntityId { get; }
}
