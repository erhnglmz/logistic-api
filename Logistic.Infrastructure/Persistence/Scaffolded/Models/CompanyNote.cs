using System;
using System.Collections.Generic;

namespace Logistic.Infrastructure.Persistence.Scaffolded.Models;

public partial class CompanyNote
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string NoteText { get; set; } = null!;

    public DateOnly? ContactDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? CreatedById { get; set; }

    public Guid? ModifiedById { get; set; }

    public virtual Company Company { get; set; } = null!;
}
