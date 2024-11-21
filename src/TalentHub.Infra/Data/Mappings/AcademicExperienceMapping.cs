using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.EducationalInstitutes;
using TalentHub.ApplicationCore.Shared.Enums;
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
            .HasOne<EducationalInstitute>()
            .WithMany()
            .HasForeignKey(p => p.InstitutionId)
            .IsRequired();
    }
}
