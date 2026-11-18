using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class AddCustomerBankAccountCommandHandler
{
    private readonly ICompanyRepository _companyRepository;

    public AddCustomerBankAccountCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CustomerBankAccountResponse> Handle(
        AddCustomerBankAccountCommand command,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCustomerByIdWithDetailsAsync(command.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Customer", command.CustomerId);

        var request = command.Request;
        var bankAccount = company.AddBankAccount(request.Iban, request.BankName, request.IsDefault, DateTime.UtcNow);
        await _companyRepository.UpdateAsync(company, cancellationToken);

        return CustomerAssembler.ToBankAccountResponse(bankAccount);
    }
}
