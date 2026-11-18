namespace Logistic.ApplicationService.Features.Customers.Queries;

public sealed class GetCustomerListQuery
{
    public string? Search { get; init; }

    public bool? IsActive { get; init; }

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public string SortBy { get; init; } = "name";

    public bool SortDescending { get; init; }
}
