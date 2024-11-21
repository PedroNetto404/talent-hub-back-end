using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;

namespace TalentHub.Infra.Data.Mappings;

public sealed class ProfessionalExperienceMapping : IEntityTypeConfiguration<ProfessionalExperience>
{
    public void Configure(EntityTypeBuilder<ProfessionalExperience> builder)
    {
        builder.HasBaseType<Experience>();

        builder
            .Property(p => p.Position)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(p => p.Company)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.Level)
            .HasConversion<EnumToStringConverter<ProfessionalLevel>>()
            .IsRequired();
    }
}
