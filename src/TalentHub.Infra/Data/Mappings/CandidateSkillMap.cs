using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Skills;

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
