using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Candidates.Entities;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CertificateMapping :
IEntityTypeConfiguration<Certificate>
{

    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.ToTable("certificates");
        builder.HasKey(p => p.Id);
        builder.Property(P => P.Id);

        builder
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(p => p.Institution)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(p => p.Workload)
            .IsRequired();
    }
}
