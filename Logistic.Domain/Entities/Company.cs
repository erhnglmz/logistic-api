using Logistic.Domain.Common;
using Logistic.Domain.Enums;
using Logistic.Domain.Exceptions;

namespace Logistic.Domain.Entities;

public sealed class Company : BaseEntity
{
    private readonly List<CompanyRole> _roles = [];
    private readonly List<CompanyContact> _contacts = [];
    private readonly List<CompanyNote> _notes = [];
    private readonly List<CompanyBankAccount> _bankAccounts = [];

    private Company()
    {
    }

    public string Name { get; private set; } = string.Empty;

    public CompanyType CompanyType { get; private set; }

    public string? TaxOffice { get; private set; }

    public string? TaxNumber { get; private set; }

    public string? NationalId { get; private set; }

    public string? Address { get; private set; }

    public string? Phone { get; private set; }

    public string? Email { get; private set; }

    public string? AccountingContactInfo { get; private set; }

    public int? DefaultPaymentTermDays { get; private set; }

    public decimal? DefaultVatRate { get; private set; }

    public bool IsActive { get; private set; } = true;

    public IReadOnlyCollection<CompanyRole> Roles => _roles.AsReadOnly();

    public IReadOnlyCollection<CompanyContact> Contacts => _contacts.AsReadOnly();

    public IReadOnlyCollection<CompanyNote> Notes => _notes.AsReadOnly();

    public IReadOnlyCollection<CompanyBankAccount> BankAccounts => _bankAccounts.AsReadOnly();

    public static Company CreateCustomer(
        string name,
        CompanyType companyType,
        string? taxOffice,
        string? taxNumber,
        string? nationalId,
        string? address,
        string? phone,
        string? email,
        string? accountingContactInfo,
        int? defaultPaymentTermDays,
        decimal? defaultVatRate,
        DateTime createdDate)
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            CompanyType = companyType,
            TaxOffice = taxOffice?.Trim(),
            TaxNumber = taxNumber?.Trim(),
            NationalId = nationalId?.Trim(),
            Address = address?.Trim(),
            Phone = phone?.Trim(),
            Email = email?.Trim(),
            AccountingContactInfo = accountingContactInfo?.Trim(),
            DefaultPaymentTermDays = defaultPaymentTermDays,
            DefaultVatRate = defaultVatRate,
            IsActive = true
        };

        company.ValidateIdentityFields();
        company.SetAuditOnCreate(createdDate);
        company.AddRole(CompanyRoleType.Customer, createdDate);

        return company;
    }

    public static Company Reconstitute(
        Guid id,
        string name,
        CompanyType companyType,
        string? taxOffice,
        string? taxNumber,
        string? nationalId,
        string? address,
        string? phone,
        string? email,
        string? accountingContactInfo,
        int? defaultPaymentTermDays,
        decimal? defaultVatRate,
        bool isActive,
        DateTime createdDate,
        DateTime? modifiedDate,
        Guid? createdById,
        Guid? modifiedById,
        IEnumerable<CompanyRole> roles,
        IEnumerable<CompanyContact> contacts,
        IEnumerable<CompanyNote> notes,
        IEnumerable<CompanyBankAccount> bankAccounts)
    {
        var company = new Company
        {
            Id = id,
            Name = name,
            CompanyType = companyType,
            TaxOffice = taxOffice,
            TaxNumber = taxNumber,
            NationalId = nationalId,
            Address = address,
            Phone = phone,
            Email = email,
            AccountingContactInfo = accountingContactInfo,
            DefaultPaymentTermDays = defaultPaymentTermDays,
            DefaultVatRate = defaultVatRate,
            IsActive = isActive,
            CreatedDate = createdDate,
            ModifiedDate = modifiedDate,
            CreatedById = createdById,
            ModifiedById = modifiedById
        };

        company._roles.AddRange(roles);
        company._contacts.AddRange(contacts);
        company._notes.AddRange(notes);
        company._bankAccounts.AddRange(bankAccounts);

        return company;
    }

    public void UpdateDetails(
        string name,
        CompanyType companyType,
        string? taxOffice,
        string? taxNumber,
        string? nationalId,
        string? address,
        string? phone,
        string? email,
        string? accountingContactInfo,
        int? defaultPaymentTermDays,
        decimal? defaultVatRate,
        DateTime modifiedDate)
    {
        EnsureActiveForMutation();

        Name = name.Trim();
        CompanyType = companyType;
        TaxOffice = taxOffice?.Trim();
        TaxNumber = taxNumber?.Trim();
        NationalId = nationalId?.Trim();
        Address = address?.Trim();
        Phone = phone?.Trim();
        Email = email?.Trim();
        AccountingContactInfo = accountingContactInfo?.Trim();
        DefaultPaymentTermDays = defaultPaymentTermDays;
        DefaultVatRate = defaultVatRate;

        ValidateIdentityFields();
        SetAuditOnUpdate(modifiedDate);
    }

    public void Deactivate(DateTime modifiedDate)
    {
        if (!IsActive)
        {
            throw new BusinessRuleException("Company is already inactive.");
        }

        IsActive = false;
        SetAuditOnUpdate(modifiedDate);
    }

    public void AddRole(CompanyRoleType role, DateTime createdDate)
    {
        if (_roles.Any(existing => existing.Role == role))
        {
            return;
        }

        _roles.Add(CompanyRole.Create(Id, role, createdDate));
        SetAuditOnUpdate(createdDate);
    }

    public CompanyContact AddContact(
        string firstName,
        string lastName,
        string? title,
        string? phone,
        string? email,
        bool isPrimary,
        DateTime createdDate)
    {
        EnsureActiveForMutation();

        if (isPrimary)
        {
            ClearPrimaryContacts(createdDate);
        }

        var contact = CompanyContact.Create(Id, firstName, lastName, title, phone, email, isPrimary, createdDate);
        _contacts.Add(contact);
        SetAuditOnUpdate(createdDate);

        return contact;
    }

    public CompanyNote AddNote(string noteText, DateOnly? contactDate, DateTime createdDate)
    {
        EnsureActiveForMutation();

        var note = CompanyNote.Create(Id, noteText, contactDate, createdDate);
        _notes.Add(note);
        SetAuditOnUpdate(createdDate);

        return note;
    }

    public CompanyBankAccount AddBankAccount(string iban, string? bankName, bool isDefault, DateTime createdDate)
    {
        EnsureActiveForMutation();

        if (isDefault)
        {
            ClearDefaultBankAccounts(createdDate);
        }

        var bankAccount = CompanyBankAccount.Create(Id, iban, bankName, isDefault, createdDate);
        _bankAccounts.Add(bankAccount);
        SetAuditOnUpdate(createdDate);

        return bankAccount;
    }

    public bool HasRole(CompanyRoleType role) => _roles.Any(existing => existing.Role == role);

    private void ValidateIdentityFields()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ValidationException("Company name is required.");
        }

        if (CompanyType == CompanyType.Individual)
        {
            if (string.IsNullOrWhiteSpace(NationalId))
            {
                throw new ValidationException("National ID is required for individual companies.");
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(TaxNumber))
            {
                throw new ValidationException("Tax number is required for corporate companies.");
            }

            if (string.IsNullOrWhiteSpace(TaxOffice))
            {
                throw new ValidationException("Tax office is required for corporate companies.");
            }
        }
    }

    private void EnsureActiveForMutation()
    {
        if (!IsActive)
        {
            throw new BusinessRuleException("Inactive companies cannot be modified.");
        }
    }

    private void ClearPrimaryContacts(DateTime modifiedDate)
    {
        foreach (var contact in _contacts.Where(contact => contact.IsPrimary))
        {
            contact.ClearPrimary(modifiedDate);
        }
    }

    private void ClearDefaultBankAccounts(DateTime modifiedDate)
    {
        foreach (var bankAccount in _bankAccounts.Where(account => account.IsDefault))
        {
            bankAccount.ClearDefault(modifiedDate);
        }
    }
}
