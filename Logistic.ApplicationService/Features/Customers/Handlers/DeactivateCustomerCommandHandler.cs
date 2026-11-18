using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class DeactivateCustomerCommandHandler
{
    private readonly ICompanyRepository _companyRepository;

    public DeactivateCustomerCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(DeactivateCustomerCommand command, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCustomerByIdWithDetailsAsync(command.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Customer", command.CustomerId);

        company.Deactivate(DateTime.UtcNow);
        await _companyRepository.UpdateAsync(company, cancellationToken);
    }
}
