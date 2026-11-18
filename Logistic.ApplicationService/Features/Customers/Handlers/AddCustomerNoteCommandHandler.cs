using Logistic.ApplicationService.Features.Customers.Assemblers;
using Logistic.ApplicationService.Features.Customers.Commands;
using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Handlers;

public sealed class AddCustomerNoteCommandHandler
{
    private readonly ICompanyRepository _companyRepository;

    public AddCustomerNoteCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CustomerNoteResponse> Handle(
        AddCustomerNoteCommand command,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCustomerByIdWithDetailsAsync(command.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Customer", command.CustomerId);

        var note = company.AddNote(command.Request.NoteText, command.Request.ContactDate, DateTime.UtcNow);
        await _companyRepository.UpdateAsync(company, cancellationToken);

        return CustomerAssembler.ToNoteResponse(note);
    }
}
