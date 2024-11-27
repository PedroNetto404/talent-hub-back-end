using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Universities;
using TalentHub.Infra.Data.ValueConverters;

namespace TalentHub.Infra.Data.Mappings;

public sealed class AcademicExperienceMapping : IEntityTypeConfiguration<AcademicExperience>
{
    public void Configure(EntityTypeBuilder<AcademicExperience> builder)
    {
        builder.HasBaseType<Experience>();

        builder
            .Property(p => p.Level)
            .HasConversion<EnumStringSnakeCaseConverter<EducationLevel>>()
            .IsRequired();

        builder
            .Property(p => p.Status)
            .HasConversion<EnumStringSnakeCaseConverter<ProgressStatus>>()
            .IsRequired();

        builder
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(p => p.CourseId)
            .IsRequired();

        builder
            .HasOne<University>()
            .WithMany()
            .HasForeignKey(p => p.UniversityId)
            .IsRequired();

        builder
            .Property<List<AcademicEntity>>("_academicEntities")
            .HasConversion(
                p => p.Select(q => q.ToString().Underscore()),
                q =>
                    q.Select(k =>
                            Enum.Parse<AcademicEntity>(k.Pascalize(), true))
                        .ToList());
    }
}