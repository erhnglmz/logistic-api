using Logistic.ApplicationService.Features.Customers.Models;

namespace Logistic.ApplicationService.Features.Customers.Commands;

public sealed class CreateCustomerCommand
{
    public required CreateCustomerRequest Request { get; init; }
}
