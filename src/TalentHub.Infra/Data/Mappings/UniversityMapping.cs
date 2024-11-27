using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Universities;

namespace TalentHub.Infra.Data.Mappings;

public sealed class EducationalInstituteMapping : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.ToTable("universities");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
            .Property(p => p.SiteUrl)
            .HasMaxLength(500);
    }
}