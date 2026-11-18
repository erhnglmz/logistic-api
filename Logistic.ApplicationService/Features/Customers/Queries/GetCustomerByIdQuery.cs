namespace Logistic.ApplicationService.Features.Customers.Queries;

public sealed class GetCustomerByIdQuery
{
    public required Guid CustomerId { get; init; }
}
