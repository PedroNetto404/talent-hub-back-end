using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.Infra.Data.Interceptors;

public sealed class VersionedEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default
    )
    {
        Debug.Assert(eventData != null);

        foreach (EntityEntry entry in eventData.Context!.ChangeTracker.Entries<VersionedEntity>())
        {
            var entity = (VersionedEntity)entry.Entity;

            MethodInfo updateVersionMethod = typeof(VersionedEntity).GetMethod(
                "UpdateVersion", 
                BindingFlags.Instance | BindingFlags.NonPublic
            )!;

            if (entity.Version is null)
            {
                entry.State = EntityState.Added;
            }

            updateVersionMethod.Invoke(entry.Entity, null);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
