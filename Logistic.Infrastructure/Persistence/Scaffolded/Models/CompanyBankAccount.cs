using System;
using System.Collections.Generic;

namespace Logistic.Infrastructure.Persistence.Scaffolded.Models;

public partial class CompanyBankAccount
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string Iban { get; set; } = null!;

    public string? BankName { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public virtual Company Company { get; set; } = null!;
}
