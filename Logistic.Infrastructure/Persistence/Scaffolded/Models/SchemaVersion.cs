using System;
using System.Collections.Generic;

namespace Logistic.Infrastructure.Persistence.Scaffolded.Models;

public partial class SchemaVersion
{
    public string Version { get; set; } = null!;

    public DateTime AppliedAt { get; set; }

    public string? Description { get; set; }
}
