using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.ApplicationService.Features.Customers.Queries;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class GetCustomerByIdQueryHandler
{
    private readonly ICompanyRepository _companyRepository;

    public GetCustomerByIdQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CustomerResponse> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCustomerByIdWithDetailsAsync(query.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Customer", query.CustomerId);

        return CustomerAssembler.ToResponse(company);
    }
}
