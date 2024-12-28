using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.Infra.Data.Mappings.Abstractions;

public abstract class AuditableAggregateRootMapping<T> : 
    EntityMapping<T> where T : AuditableAggregateRoot
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.CreatedAtUtc)   
            .IsRequired()
            .HasDefaultValue(System.DateTime.UtcNow);

        builder
            .Property(p => p.UpdatedAtUtc)
            .IsRequired()
            .HasDefaultValue(System.DateTime.UtcNow);

        builder
            .Property(p => p.DeletedAtUtc);

        builder.HasQueryFilter(p => p.DeletedAtUtc == null);            
    }
}
