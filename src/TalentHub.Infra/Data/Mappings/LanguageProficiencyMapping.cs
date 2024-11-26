using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Skills.Enums;
using TalentHub.Infra.Data.ValueConverters;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateLanguageSkillMapping :
    IEntityTypeConfiguration<LanguageProficiency>
{
    public void Configure(EntityTypeBuilder<LanguageProficiency> builder)
    {
        builder.ToTable("language_proficiencies");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        var defaultValue = Proficiency.Beginner;

        builder
            .Property(p => p.WritingLevel)
            .HasConversion<EnumStringSnakeCaseConverter<Proficiency>>()
            .HasDefaultValue(defaultValue)
            .IsRequired();

        builder
            .Property(p => p.SpeakingLevel)
            .HasConversion<EnumStringSnakeCaseConverter<Proficiency>>()
            .HasDefaultValue(defaultValue)
            .IsRequired();

        builder
            .Property(p => p.ListeningLevel)
            .HasConversion<EnumStringSnakeCaseConverter<Proficiency>>()
            .IsRequired()
            .HasDefaultValue(defaultValue);

        builder
            .Property(p => p.Language)
            .IsRequired()
            .HasConversion(
                p => p.Name.Underscore(), 
                q => Language.FromName(q.Pascalize(), true)
            );
    }
}
