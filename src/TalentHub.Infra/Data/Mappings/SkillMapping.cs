using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Skills;

namespace TalentHub.Infra.Data.Mappings;

public sealed class SkillMapping : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder.Property(p => p.Type).IsRequired();
        builder.Property(p => p.Name).IsRequired();

        builder.Property("_tags").HasColumnType("text[]");

        builder.Property(p => p.Approved).IsRequired();
        builder.Ignore(p => p.IsSuggestion);
    }
}