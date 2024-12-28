using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.Infra.Data.Mappings.Abstractions;

public abstract class VersionedEntityMapping<T> : EntityMapping<T> where T : VersionedEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.Version).IsRowVersion().HasDefaultValue(0).IsRequired();
    }
}
