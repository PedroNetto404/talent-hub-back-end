using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages;
using TalentHub.Infra.Data.Mappings.Abstractions;
using TalentHub.Infra.Data.ValueConverters;

namespace TalentHub.Infra.Data.Mappings.CandidateAggregate;

public sealed class CandidateLanguageSkillMapping :
    VersionedEntityMapping<LanguageProficiency>
{
    public override void Configure(EntityTypeBuilder<LanguageProficiency> builder)
    {
        base.Configure(builder);

        builder.ToTable("language_proficiencies");

        builder
            .Property(p => p.Language)
            .IsRequired()
            .HasConversion(
                p => p.Name.Underscore(),
                q => Language.FromName(q.Pascalize(), true)
            );

        Proficiency defaultValue = Proficiency.Beginner;

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
    }
}
