using Logistic.ApplicationService.Features.Customers.Models;
using Logistic.Domain.Entities;

namespace Logistic.ApplicationService.Features.Customers.Assemblers;

public static class CustomerAssembler
{
    public static CustomerResponse ToResponse(Company company) =>
        new()
        {
            Id = company.Id,
            Name = company.Name,
            CompanyType = company.CompanyType.ToString(),
            TaxOffice = company.TaxOffice,
            TaxNumber = company.TaxNumber,
            NationalId = company.NationalId,
            Address = company.Address,
            Phone = company.Phone,
            Email = company.Email,
            AccountingContactInfo = company.AccountingContactInfo,
            DefaultPaymentTermDays = company.DefaultPaymentTermDays,
            DefaultVatRate = company.DefaultVatRate,
            IsActive = company.IsActive,
            CreatedDate = company.CreatedDate,
            ModifiedDate = company.ModifiedDate,
            Roles = company.Roles.Select(role => role.Role.ToString()).ToList(),
            Contacts = company.Contacts.Select(ToContactResponse).ToList(),
            Notes = company.Notes.Select(ToNoteResponse).ToList(),
            BankAccounts = company.BankAccounts.Select(ToBankAccountResponse).ToList()
        };

    public static CustomerListItemResponse ToListItemResponse(Company company) =>
        new()
        {
            Id = company.Id,
            Name = company.Name,
            CompanyType = company.CompanyType.ToString(),
            TaxNumber = company.TaxNumber,
            NationalId = company.NationalId,
            Phone = company.Phone,
            Email = company.Email,
            IsActive = company.IsActive,
            CreatedDate = company.CreatedDate
        };

    public static CustomerContactResponse ToContactResponse(CompanyContact contact) =>
        new()
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Title = contact.Title,
            Phone = contact.Phone,
            Email = contact.Email,
            IsPrimary = contact.IsPrimary
        };

    public static CustomerNoteResponse ToNoteResponse(CompanyNote note) =>
        new()
        {
            Id = note.Id,
            NoteText = note.NoteText,
            ContactDate = note.ContactDate,
            CreatedDate = note.CreatedDate
        };

    public static CustomerBankAccountResponse ToBankAccountResponse(CompanyBankAccount bankAccount) =>
        new()
        {
            Id = bankAccount.Id,
            Iban = bankAccount.Iban,
            BankName = bankAccount.BankName,
            IsDefault = bankAccount.IsDefault
        };
}
