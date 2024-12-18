using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills;
using TalentHub.ApplicationCore.Resources.Skills;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateSkillMap :
    EntityMapping<CandidateSkill>
{
    public override void Configure(EntityTypeBuilder<CandidateSkill> builder)
    {
        base.Configure(builder);

        builder.ToTable("candidate_skills");

        builder.Property(c => c.Proficiency)
               .HasConversion<EnumToStringConverter<Proficiency>>()
               .IsRequired();

        builder
            .HasOne<Skill>()
            .WithMany()
            .HasForeignKey(p => p.SkillId);
    }
}
