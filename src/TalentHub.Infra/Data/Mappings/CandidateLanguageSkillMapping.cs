using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateLanguageSkillMapping :
    IEntityTypeConfiguration<LanguageProficiency>
{
    public void Configure(EntityTypeBuilder<LanguageProficiency> builder)
    {
        builder.HasBaseType<CandidateSkill>();

        builder
            .Property(p => p.SpecialProficiences)
            .HasField("_specialProficiency")
            .HasColumnName("special_proficiences")
            .HasColumnType("jsonb")
            .HasConversion(
                p => p.ToJson(),
                q => q.FromJson<Dictionary<LanguageSkillType, Proficiency>>());
    }
}
