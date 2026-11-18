using Logistic.Domain.Entities;
using Logistic.Domain.Enums;
using PersistenceCompany = Logistic.Infrastructure.Persistence.Scaffolded.Models.Company;
using PersistenceCompanyBankAccount = Logistic.Infrastructure.Persistence.Scaffolded.Models.CompanyBankAccount;
using PersistenceCompanyContact = Logistic.Infrastructure.Persistence.Scaffolded.Models.CompanyContact;
using PersistenceCompanyNote = Logistic.Infrastructure.Persistence.Scaffolded.Models.CompanyNote;
using PersistenceCompanyRole = Logistic.Infrastructure.Persistence.Scaffolded.Models.CompanyRole;

namespace Logistic.Infrastructure.Persistence.Repositories.Mapper;

public static class CompanyEntityMap
{
    public static Company ToDomain(PersistenceCompany source)
    {
        var roles = source.CompanyRoles.Select(ToDomainRole).ToList();
        var contacts = source.CompanyContacts.Select(ToDomainContact).ToList();
        var notes = source.CompanyNotes.Select(ToDomainNote).ToList();
        var bankAccounts = source.CompanyBankAccounts.Select(ToDomainBankAccount).ToList();

        return Company.Reconstitute(
            source.Id,
            source.Name,
            ParseCompanyType(source.CompanyType),
            source.TaxOffice,
            source.TaxNumber,
            source.NationalId,
            source.Address,
            source.Phone,
            source.Email,
            source.AccountingContactInfo,
            source.DefaultPaymentTermDays,
            source.DefaultVatRate,
            source.IsActive,
            source.CreatedDate,
            source.ModifiedDate,
            source.CreatedById,
            source.ModifiedById,
            roles,
            contacts,
            notes,
            bankAccounts);
    }

    public static PersistenceCompany ToPersistence(Company source, PersistenceCompany? existing = null)
    {
        var target = existing ?? new PersistenceCompany
        {
            Id = source.Id,
            CreatedDate = source.CreatedDate,
            CreatedById = source.CreatedById
        };

        target.Name = source.Name;
        target.CompanyType = source.CompanyType.ToString();
        target.TaxOffice = source.TaxOffice;
        target.TaxNumber = source.TaxNumber;
        target.NationalId = source.NationalId;
        target.Address = source.Address;
        target.Phone = source.Phone;
        target.Email = source.Email;
        target.AccountingContactInfo = source.AccountingContactInfo;
        target.DefaultPaymentTermDays = source.DefaultPaymentTermDays;
        target.DefaultVatRate = source.DefaultVatRate;
        target.IsActive = source.IsActive;
        target.ModifiedDate = source.ModifiedDate;
        target.ModifiedById = source.ModifiedById;

        SyncRoles(source, target);
        SyncContacts(source, target);
        SyncNotes(source, target);
        SyncBankAccounts(source, target);

        return target;
    }

    private static void SyncRoles(Company source, PersistenceCompany target)
    {
        var existingById = target.CompanyRoles.ToDictionary(role => role.Id);
        var desiredIds = source.Roles.Select(role => role.Id).ToHashSet();

        foreach (var role in target.CompanyRoles.Where(role => !desiredIds.Contains(role.Id)).ToList())
        {
            target.CompanyRoles.Remove(role);
        }

        foreach (var role in source.Roles)
        {
            if (existingById.TryGetValue(role.Id, out var existingRole))
            {
                existingRole.Role = role.Role.ToString();
                existingRole.ModifiedDate = role.ModifiedDate;
                existingRole.ModifiedById = role.ModifiedById;
            }
            else
            {
                target.CompanyRoles.Add(new PersistenceCompanyRole
                {
                    Id = role.Id,
                    CompanyId = source.Id,
                    Role = role.Role.ToString(),
                    CreatedDate = role.CreatedDate,
                    ModifiedDate = role.ModifiedDate,
                    CreatedById = role.CreatedById,
                    ModifiedById = role.ModifiedById
                });
            }
        }
    }

    private static void SyncContacts(Company source, PersistenceCompany target)
    {
        var existingById = target.CompanyContacts.ToDictionary(contact => contact.Id);
        var desiredIds = source.Contacts.Select(contact => contact.Id).ToHashSet();

        foreach (var contact in target.CompanyContacts.Where(contact => !desiredIds.Contains(contact.Id)).ToList())
        {
            target.CompanyContacts.Remove(contact);
        }

        foreach (var contact in source.Contacts)
        {
            if (existingById.TryGetValue(contact.Id, out var existingContact))
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Title = contact.Title;
                existingContact.Phone = contact.Phone;
                existingContact.Email = contact.Email;
                existingContact.IsPrimary = contact.IsPrimary;
                existingContact.ModifiedDate = contact.ModifiedDate;
                existingContact.ModifiedById = contact.ModifiedById;
            }
            else
            {
                target.CompanyContacts.Add(new PersistenceCompanyContact
                {
                    Id = contact.Id,
                    CompanyId = source.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Title = contact.Title,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    IsPrimary = contact.IsPrimary,
                    CreatedDate = contact.CreatedDate,
                    ModifiedDate = contact.ModifiedDate,
                    CreatedById = contact.CreatedById,
                    ModifiedById = contact.ModifiedById
                });
            }
        }
    }

    private static void SyncNotes(Company source, PersistenceCompany target)
    {
        var existingById = target.CompanyNotes.ToDictionary(note => note.Id);
        var desiredIds = source.Notes.Select(note => note.Id).ToHashSet();

        foreach (var note in target.CompanyNotes.Where(note => !desiredIds.Contains(note.Id)).ToList())
        {
            target.CompanyNotes.Remove(note);
        }

        foreach (var note in source.Notes)
        {
            if (existingById.TryGetValue(note.Id, out var existingNote))
            {
                existingNote.NoteText = note.NoteText;
                existingNote.ContactDate = note.ContactDate;
                existingNote.ModifiedDate = note.ModifiedDate;
                existingNote.ModifiedById = note.ModifiedById;
            }
            else
            {
                target.CompanyNotes.Add(new PersistenceCompanyNote
                {
                    Id = note.Id,
                    CompanyId = source.Id,
                    NoteText = note.NoteText,
                    ContactDate = note.ContactDate,
                    CreatedDate = note.CreatedDate,
                    ModifiedDate = note.ModifiedDate,
                    CreatedById = note.CreatedById,
                    ModifiedById = note.ModifiedById
                });
            }
        }
    }

    private static void SyncBankAccounts(Company source, PersistenceCompany target)
    {
        var existingById = target.CompanyBankAccounts.ToDictionary(account => account.Id);
        var desiredIds = source.BankAccounts.Select(account => account.Id).ToHashSet();

        foreach (var account in target.CompanyBankAccounts.Where(account => !desiredIds.Contains(account.Id)).ToList())
        {
            target.CompanyBankAccounts.Remove(account);
        }

        foreach (var account in source.BankAccounts)
        {
            if (existingById.TryGetValue(account.Id, out var existingAccount))
            {
                existingAccount.Iban = account.Iban;
                existingAccount.BankName = account.BankName;
                existingAccount.IsDefault = account.IsDefault;
                existingAccount.ModifiedDate = account.ModifiedDate;
                existingAccount.ModifiedById = account.ModifiedById;
            }
            else
            {
                target.CompanyBankAccounts.Add(new PersistenceCompanyBankAccount
                {
                    Id = account.Id,
                    CompanyId = source.Id,
                    Iban = account.Iban,
                    BankName = account.BankName,
                    IsDefault = account.IsDefault,
                    CreatedDate = account.CreatedDate,
                    ModifiedDate = account.ModifiedDate,
                    CreatedById = account.CreatedById,
                    ModifiedById = account.ModifiedById
                });
            }
        }
    }

    private static CompanyRole ToDomainRole(PersistenceCompanyRole source) =>
        CompanyRole.Reconstitute(
            source.Id,
            source.CompanyId,
            ParseRoleType(source.Role),
            source.CreatedDate,
            source.ModifiedDate,
            source.CreatedById,
            source.ModifiedById);

    private static CompanyContact ToDomainContact(PersistenceCompanyContact source) =>
        CompanyContact.Reconstitute(
            source.Id,
            source.CompanyId,
            source.FirstName,
            source.LastName,
            source.Title,
            source.Phone,
            source.Email,
            source.IsPrimary,
            source.CreatedDate,
            source.ModifiedDate,
            source.CreatedById,
            source.ModifiedById);

    private static CompanyNote ToDomainNote(PersistenceCompanyNote source) =>
        CompanyNote.Reconstitute(
            source.Id,
            source.CompanyId,
            source.NoteText,
            source.ContactDate,
            source.CreatedDate,
            source.ModifiedDate,
            source.CreatedById,
            source.ModifiedById);

    private static CompanyBankAccount ToDomainBankAccount(PersistenceCompanyBankAccount source) =>
        CompanyBankAccount.Reconstitute(
            source.Id,
            source.CompanyId,
            source.Iban,
            source.BankName,
            source.IsDefault,
            source.CreatedDate,
            source.ModifiedDate,
            source.CreatedById,
            source.ModifiedById);

    private static CompanyType ParseCompanyType(string value) =>
        Enum.TryParse<CompanyType>(value, ignoreCase: true, out var companyType)
            ? companyType
            : throw new InvalidOperationException($"Unknown company type '{value}'.");

    private static CompanyRoleType ParseRoleType(string value) =>
        Enum.TryParse<CompanyRoleType>(value, ignoreCase: true, out var roleType)
            ? roleType
            : throw new InvalidOperationException($"Unknown company role '{value}'.");
}
