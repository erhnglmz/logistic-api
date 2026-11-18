using System;
using System.Collections.Generic;

namespace Logistic.Infrastructure.Persistence.Scaffolded.Models;

public partial class CompanyContact
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Title { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool IsPrimary { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public virtual Company Company { get; set; } = null!;
}
