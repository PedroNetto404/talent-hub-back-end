using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences;
using TalentHub.ApplicationCore.Resources.Courses;
using TalentHub.ApplicationCore.Resources.Universities;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.Infra.Data.ValueConverters;

namespace TalentHub.Infra.Data.Mappings;

public sealed class AcademicExperienceMapping : IEntityTypeConfiguration<AcademicExperience>
{
    public void Configure(EntityTypeBuilder<AcademicExperience> builder)
    {
        builder.HasBaseType<Experience>();

        builder.ToTable("academic_experiences");

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
                p => p.Select(q => q.ToString().Underscore()).ToList(),
                q => q.Select(k => Enum.Parse<AcademicEntity>(k.Pascalize(), true)).ToList(),
                new ValueComparer<List<AcademicEntity>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            )
            .HasColumnType("text[]")
            .HasColumnName("academic_entites");

        builder.OwnsOne(p => p.ExpectedGraduation, b =>
        {
            b.Property(q => q.Year)
                .HasColumnName("expected_graduation_year")
                .IsRequired();

            b.Property(q => q.Month)
                .HasColumnName("expected_graduation_month")
                .IsRequired();
        });

        builder.Property(p => p.CurrentSemester)
            .HasColumnName("current_semester")
            .IsRequired();
    }
}
