using Logistic.ApplicationService.Features.Customers.Models;

namespace Logistic.ApplicationService.Features.Customers.Commands;

public sealed class AddCustomerContactCommand
{
    public required Guid CustomerId { get; init; }

    public required AddCustomerContactRequest Request { get; init; }
}
