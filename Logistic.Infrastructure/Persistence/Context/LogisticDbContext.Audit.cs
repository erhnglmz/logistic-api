using Logistic.Infrastructure.Persistence.Scaffolded.Models;
using Microsoft.EntityFrameworkCore;

namespace Logistic.Infrastructure.Persistence.Context;

public partial class LogisticDbContext
{
    public override int SaveChanges()
    {
        ApplyAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditFields()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not Company &&
                entry.Entity is not CompanyRole &&
                entry.Entity is not CompanyContact &&
                entry.Entity is not CompanyNote &&
                entry.Entity is not CompanyBankAccount)
            {
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    SetProperty(entry, "CreatedDate", utcNow);
                    break;
                case EntityState.Modified:
                    SetProperty(entry, "ModifiedDate", utcNow);
                    break;
            }
        }
    }

    private static void SetProperty(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, string propertyName, object value)
    {
        var property = entry.Properties.FirstOrDefault(item => item.Metadata.Name == propertyName);
        if (property is not null && (property.CurrentValue is null || propertyName == "ModifiedDate"))
        {
            property.CurrentValue = value;
        }
    }
}
