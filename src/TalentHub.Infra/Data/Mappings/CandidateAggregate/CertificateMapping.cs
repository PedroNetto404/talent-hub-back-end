using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates;
using TalentHub.Infra.Data.Mappings.Abstractions;

namespace TalentHub.Infra.Data.Mappings.CandidateAggregate;

public sealed class CertificateMapping :
    VersionedEntityMapping<Certificate>
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
