using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences;

namespace TalentHub.Infra.Data.Mappings.CandidateAggregate;

public sealed class ProfessionalExperienceMapping : IEntityTypeConfiguration<ProfessionalExperience>
{
    public void Configure(EntityTypeBuilder<ProfessionalExperience> builder)
    {
        builder.HasBaseType<Experience>();

        builder.ToTable("professional_experiences");

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
