using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.ApplicationService.Features.Customers.Services;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class UpdateCustomerCommandHandler
{
    private readonly ICompanyRepository _companyRepository;
    private readonly CustomerValidationService _validationService;

    public UpdateCustomerCommandHandler(
        ICompanyRepository companyRepository,
        CustomerValidationService validationService)
    {
        _companyRepository = companyRepository;
        _validationService = validationService;
    }

    public async Task<CustomerResponse> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCustomerByIdWithDetailsAsync(command.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Customer", command.CustomerId);

        var request = command.Request;
        var companyType = _validationService.ParseCompanyType(request.CompanyType);

        await _validationService.EnsureUniqueIdentifiersAsync(
            companyType,
            request.TaxNumber,
            request.NationalId,
            command.CustomerId,
            cancellationToken);

        company.UpdateDetails(
            request.Name,
            companyType,
            request.TaxOffice,
            request.TaxNumber,
            request.NationalId,
            request.Address,
            request.Phone,
            request.Email,
            request.AccountingContactInfo,
            request.DefaultPaymentTermDays,
            request.DefaultVatRate,
            DateTime.UtcNow);

        await _companyRepository.UpdateAsync(company, cancellationToken);

        return CustomerAssembler.ToResponse(company);
    }
}
