using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Candidates.ValueObjects;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.EducationalInstitutes;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Skills;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateMapping :
    IEntityTypeConfiguration<Candidate>,
    IEntityTypeConfiguration<CandidateSkill>,
    IEntityTypeConfiguration<CandidateLanguageSkill>,
    IEntityTypeConfiguration<Experience>,
    IEntityTypeConfiguration<AcademicExperience>,
    IEntityTypeConfiguration<ProfessionalExperience>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(200);
        builder.HasIndex(p => p.Email)
               .IsUnique();

        builder.Property(c => c.Phone)
               .IsRequired()
               .HasMaxLength(11);
        builder.HasIndex(c => c.Phone)
               .IsUnique();

        builder.OwnsOne(c => c.Address, q =>
        {
            q.Property(a => a.Street)
             .IsRequired()
             .HasMaxLength(200);

            q.Property(a => a.ZipCode)
             .IsRequired()
             .HasMaxLength(8);

            q.Property(a => a.Country)
             .IsRequired()
             .HasMaxLength(20);

            q.Property(a => a.Number)
             .IsRequired()
             .HasMaxLength(15);

            q.Property(a => a.Neighborhood)
             .IsRequired()
             .HasMaxLength(50);

            q.Property(a => a.State)
             .IsRequired()
             .HasMaxLength(20);
        });

        builder.Ignore(c => c.Age);

        builder.Property(p => p.BirthDate)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(c => c.Summary)
               .HasMaxLength(200);

        builder.Property(c => c.ResumeUrl)
               .HasMaxLength(500);

        builder.Property(c => c.GithubUrl)
               .HasMaxLength(500);

        builder.Property(c => c.InstagramUrl)
               .HasMaxLength(500);

        builder.Property(c => c.LinkedinUrl)
               .HasMaxLength(500);

        builder.Property(c => c.ExpectedRemuneration)
               .HasPrecision(12, 2);

        builder.Property(p => p.Skills)
               .HasField("_skills")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany<CandidateSkill>("_skills")
               .WithOne()
               .HasForeignKey("candidate_id")
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Experiences)
               .HasField("_experiences")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany<Experience>("_experiences")
               .WithOne()
               .HasForeignKey("candidate_id")
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Certificates)
               .HasField("_certificates")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany<Certificate>("_certificates")
               .WithOne()
               .HasForeignKey("candidate_id")
               .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<CandidateSkill> builder)
    {
        builder.ToTable("candidate_skills");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(c => c.SkillId)
               .IsRequired();

        builder.Property(c => c.Proficiency)
               .HasConversion<EnumToStringConverter<Proficiency>>()
               .IsRequired();

        builder.HasDiscriminator<string>("skill_type")
               .HasValue<CandidateSkill>("common")
               .HasValue<CandidateLanguageSkill>("language");

        builder.HasOne<Skill>()
               .WithMany()
               .HasForeignKey(p => p.SkillId);
    }

    public void Configure(EntityTypeBuilder<CandidateLanguageSkill> builder)
    {
        builder.HasBaseType<CandidateSkill>();

        builder.Property(p => p.SpecialProficiences)
               .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                    v => JsonSerializer.Deserialize<Dictionary<LanguageSkillType, Proficiency>>(
                        v,
                        new JsonSerializerOptions())!)
               .HasColumnType("jsonb")
               .IsRequired();
    }

    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder.OwnsOne(p => p.Start, q =>
        {
            q.Property(k => k.Year)
             .IsRequired()
             .HasColumnName("start_year");

            q.Property(k => k.Month)
             .IsRequired()
             .HasColumnName("start_month");
        });

        builder.OwnsOne(p => p.End, q =>
        {
            q.Property(k => k.Year)
             .IsRequired()
             .HasColumnName("end_year");

            q.Property(k => k.Month)
             .IsRequired()
             .HasColumnName("end_month");
        });

        builder.Property(p => p.IsCurrent)
               .IsRequired();

        builder.Property("_activities")
               .HasColumnType("text[]");

        builder.HasDiscriminator<string>("experience_type")
               .HasValue<AcademicExperience>("academic")
               .HasValue<ProfessionalExperience>("professional");
    }

    public void Configure(EntityTypeBuilder<AcademicExperience> builder)
    {
        builder.HasBaseType<Experience>();

        builder.Property(p => p.Level)
               .HasConversion<StringToEnumConverter<EducationLevel>>()
               .IsRequired();

        builder.Property(p => p.Status)
               .HasConversion<StringToEnumConverter<ProgressStatus>>()
               .IsRequired();

        builder.Property(p => p.CourseId)
               .IsRequired();

        builder.HasOne<Course>()
               .WithMany()
               .HasForeignKey(p => p.CourseId)
               .IsRequired();

        builder.Property(p => p.InstitutionId)
               .IsRequired();

        builder.HasOne<EducationalInstitute>()
               .WithMany()
               .HasForeignKey(p => p.InstitutionId)
               .IsRequired();
    }

    public void Configure(EntityTypeBuilder<ProfessionalExperience> builder)
    {
        builder.HasBaseType<Experience>();

        builder.Property(p => p.Position)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Company)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Level)
               .HasConversion<StringToEnumConverter<ProfessionalLevel>>()
               .IsRequired();
    }
}
