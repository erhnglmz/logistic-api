using Logistic.ApplicationService.Features.Customers.Models;

namespace Logistic.ApplicationService.Features.Customers.Commands;

public sealed class UpdateCustomerCommand
{
    public required Guid CustomerId { get; init; }

    public required UpdateCustomerRequest Request { get; init; }
}
