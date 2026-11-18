using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.ApplicationService.Features.Customers.Services;
using Logistic.Domain.Entities;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class CreateCustomerCommandHandler
{
    private readonly ICompanyRepository _companyRepository;
    private readonly CustomerValidationService _validationService;

    public CreateCustomerCommandHandler(
        ICompanyRepository companyRepository,
        CustomerValidationService validationService)
    {
        _companyRepository = companyRepository;
        _validationService = validationService;
    }

    public async Task<CustomerResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var companyType = _validationService.ParseCompanyType(request.CompanyType);

        await _validationService.EnsureUniqueIdentifiersAsync(
            companyType,
            request.TaxNumber,
            request.NationalId,
            excludeCompanyId: null,
            cancellationToken);

        var company = Company.CreateCustomer(
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

        await _companyRepository.AddAsync(company, cancellationToken);

        return CustomerAssembler.ToResponse(company);
    }
}
