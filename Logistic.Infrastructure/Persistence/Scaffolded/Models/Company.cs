using System;
using System.Collections.Generic;

namespace Logistic.Infrastructure.Persistence.Scaffolded.Models;

public partial class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string CompanyType { get; set; } = null!;

    public string? TaxOffice { get; set; }

    public string? TaxNumber { get; set; }

    public string? NationalId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? AccountingContactInfo { get; set; }

    public int? DefaultPaymentTermDays { get; set; }

    public decimal? DefaultVatRate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public virtual ICollection<CompanyBankAccount> CompanyBankAccounts { get; set; } = new List<CompanyBankAccount>();

    public virtual ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();

    public virtual ICollection<CompanyNote> CompanyNotes { get; set; } = new List<CompanyNote>();

    public virtual ICollection<CompanyRole> CompanyRoles { get; set; } = new List<CompanyRole>();
}
