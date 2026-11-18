using Logistic.ApplicationService.Features.Customers.Models;

namespace Logistic.ApplicationService.Features.Customers.Commands;

public sealed class AddCustomerBankAccountCommand
{
    public required Guid CustomerId { get; init; }

    public required AddCustomerBankAccountRequest Request { get; init; }
}
