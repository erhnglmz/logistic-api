using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class AddCustomerContactCommandHandler
{
    private readonly ICompanyRepository _companyRepository;

    public AddCustomerContactCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CustomerContactResponse> Handle(
        AddCustomerContactCommand command,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCustomerByIdWithDetailsAsync(command.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Customer", command.CustomerId);

        var request = command.Request;
        var contact = company.AddContact(
            request.FirstName,
            request.LastName,
            request.Title,
            request.Phone,
            request.Email,
            request.IsPrimary,
            DateTime.UtcNow);

        await _companyRepository.UpdateAsync(company, cancellationToken);

        return CustomerAssembler.ToContactResponse(contact);
    }
}
