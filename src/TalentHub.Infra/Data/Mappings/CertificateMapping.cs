using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Candidates.Entities;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CertificateMapping :
    EntityMapping<Certificate>
{

    public override void Configure(EntityTypeBuilder<Certificate> builder)
    {
        base.Configure(builder);

        builder.ToTable("certificates");

        builder
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(p => p.Issuer)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(p => p.Workload)
            .IsRequired();
    }
}
