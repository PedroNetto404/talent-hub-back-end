using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences;

namespace TalentHub.Infra.Data.Mappings;

public sealed class ExperienceMapping : EntityMapping<Experience>
{
    public override void Configure(EntityTypeBuilder<Experience> builder)
    {
        base.Configure(builder);

        builder.ToTable("experiences");

        builder.OwnsOne(p => p.Start, q =>
        {
            q
            .Property(k => k.Year)
            .IsRequired()
            .HasColumnName("start_year");

            q
            .Property(k => k.Month)
            .IsRequired()
            .HasColumnName("start_month");
        });

        builder.OwnsOne(p => p.End, q =>
        {
            q
            .Property(k => k.Year)
            .IsRequired().HasColumnName("end_year");

            q
            .Property(k => k.Month)
            .IsRequired()
            .HasColumnName("end_month");
        });

        builder
            .Property(p => p.IsCurrent)
            .IsRequired();

        builder
            .Property("_activities")
            .HasColumnType("text[]")
            .HasColumnName("activities");

        builder
            .HasDiscriminator<string>("experience_type")
            .HasValue<AcademicExperience>("academic")
            .HasValue<ProfessionalExperience>("professional");
    }
}
