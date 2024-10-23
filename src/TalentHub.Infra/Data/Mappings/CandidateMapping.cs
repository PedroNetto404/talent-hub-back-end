using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateMapping : 
    IEntityTypeConfiguration<Candidate>, 
    IEntityTypeConfiguration<CandidateSkill>, 
    IEntityTypeConfiguration<CandidateLanguageSkill>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(c => c.Phone).IsRequired().HasMaxLength(11);
        builder.HasIndex(c => c.Phone).IsUnique();
        builder.OwnsOne(c => c.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street).IsRequired().HasMaxLength(200);
            addressBuilder.Property(a => a.ZipCode).IsRequired().HasMaxLength(8);
            addressBuilder.Property(a => a.Country).IsRequired().HasMaxLength(20);
            addressBuilder.Property(a => a.Number).IsRequired().HasMaxLength(15);
            addressBuilder.Property(a => a.Neighborhood).IsRequired().HasMaxLength(50);
            addressBuilder.Property(a => a.State).IsRequired().HasMaxLength(20);
        });
        builder.Ignore(c => c.Age);
        builder.Property(p => p.BirthDate).HasColumnType("date").IsRequired();
        builder.Property(c => c.Summary).HasMaxLength(200);
        builder.Property(c => c.ResumeUrl).HasMaxLength(500);
        builder.Property(c => c.GithubUrl).HasMaxLength(500);
        builder.Property(c => c.InstagramUrl).HasMaxLength(500);
        builder.Property(c => c.LinkedinUrl).HasMaxLength(500);
        builder.Property(c => c.ExpectedRemuneration).HasPrecision(12, 2);
        builder.Property(c => c.DesiredJobType).HasConversion(new EnumToStringConverter<JobType>());
        builder.Property(c => c.DesiredWorkPlaceType).HasConversion(new EnumToStringConverter<WorkplaceType>());
        builder.Property(c => c.Hobbies).HasColumnType("text[]");

        builder.HasMany<CandidateSkill>("_skills").WithOne().OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<CandidateSkill> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);
        builder.Property(c => c.SkillId).IsRequired();
        builder.Property(c => c.SkillName).IsRequired();
        builder.Property(c => c.SkillType).HasConversion(new EnumToStringConverter<SkillType>()).IsRequired();
        builder.Property(c => c.Proficiency).HasConversion(new EnumToStringConverter<Proficiency>()).IsRequired();
        builder.HasDiscriminator<string>("skill_type").HasValue<CandidateSkill>("hard_or_soft_skill").HasValue<CandidateLanguageSkill>("language_skill");
    }

    public void Configure(EntityTypeBuilder<CandidateLanguageSkill> builder)
    {
        builder.HasBaseType<CandidateSkill>();
        builder.Property(p => p.SpecialProficiences)
            .HasConversion(v =>
                    JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                v => JsonSerializer.Deserialize<Dictionary<LanguageSkillType, Proficiency>>(v,
                    new JsonSerializerOptions())!)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}