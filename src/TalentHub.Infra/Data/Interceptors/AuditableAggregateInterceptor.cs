using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.Infra.Data.Interceptors;

public class AuditableAggregateInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        foreach (EntityEntry<AuditableAggregateRoot> entry in eventData.Context!.ChangeTracker
                     .Entries<AuditableAggregateRoot>())
        {
            if (entry.State == EntityState.Deleted)
            {
                 typeof(AuditableAggregateRoot)
                    .GetMethod(
                        "OnDelete",
                        BindingFlags.Instance | BindingFlags.NonPublic
                    )!
                    .Invoke(entry.Entity, null);
                
                entry.State = EntityState.Modified;
            }

            if (entry.State == EntityState.Modified)
            {
                typeof(AuditableAggregateRoot)
                    .GetMethod(
                        "OnUpdate",
                        BindingFlags.Instance | BindingFlags.NonPublic
                    )!
                    .Invoke(entry.Entity, null);
            }
        }

        return base.SavingChanges(eventData, result);
    }
}
