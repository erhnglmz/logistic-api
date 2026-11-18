namespace Logistic.ApplicationService.Features.Customers.Commands;

public sealed class DeactivateCustomerCommand
{
    public required Guid CustomerId { get; init; }
}
