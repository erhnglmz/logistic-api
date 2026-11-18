using Logistic.ApplicationService.Common;
using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.ApplicationService.Features.Customers.Queries;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class GetCustomerListQueryHandler
{
    private readonly ICompanyRepository _companyRepository;

    public GetCustomerListQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<PagedResponse<CustomerListItemResponse>> Handle(
        GetCustomerListQuery query,
        CancellationToken cancellationToken)
    {
        var filter = new CustomerListFilter
        {
            Search = query.Search,
            IsActive = query.IsActive,
            Page = query.Page,
            PageSize = query.PageSize,
            SortBy = query.SortBy,
            SortDescending = query.SortDescending
        };

        var (items, total) = await _companyRepository.GetCustomersAsync(filter, cancellationToken);

        return new PagedResponse<CustomerListItemResponse>
        {
            Items = items.Select(CustomerAssembler.ToListItemResponse).ToList(),
            Total = total,
            Page = Math.Max(query.Page, 1),
            PageSize = Math.Clamp(query.PageSize, 1, 100)
        };
    }
}
