using Logistic.ApplicationService.Features.Customers.Models;

namespace Logistic.ApplicationService.Features.Customers.Commands;

public sealed class AddCustomerNoteCommand
{
    public required Guid CustomerId { get; init; }

    public required AddCustomerNoteRequest Request { get; init; }
}
