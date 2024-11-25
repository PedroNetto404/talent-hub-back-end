using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateLanguageSkillMapping :
    IEntityTypeConfiguration<LanguageProficiency>
{
    public void Configure(EntityTypeBuilder<LanguageProficiency> builder)
    {
        builder.ToTable("language_proficiencies");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder
            .Property(p => p.WritingLevel)
            .HasConversion<EnumToStringConverter<Proficiency>>()
            .HasDefaultValue(Proficiency.Beginner)
            .IsRequired();
        
        builder
            .Property(p => p.SpeakingLevel)
            .HasConversion<EnumToStringConverter<Proficiency>>()
            .HasDefaultValue(Proficiency.Beginner)
            .IsRequired();

        builder
            .Property(p => p.ListeningLevel)
            .HasConversion<EnumToStringConverter<Proficiency>>()
            .IsRequired()
            .HasDefaultValue(Proficiency.Beginner);

        builder
            .Property(p => p.Language)
            .IsRequired()
            .HasConversion(p => p.Name, q => Language.FromName(q, true));
    }
}
