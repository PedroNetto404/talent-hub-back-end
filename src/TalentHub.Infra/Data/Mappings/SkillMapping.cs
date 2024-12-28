using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.CompanySectors;
using TalentHub.ApplicationCore.Resources.Skills;
using TalentHub.ApplicationCore.Resources.Skills.Enums;
using TalentHub.Infra.Data.Mappings.Abstractions;
using TalentHub.Infra.Data.ValueConverters;

namespace TalentHub.Infra.Data.Mappings;

public sealed class SkillMapping : EntityMapping<Skill>
{
    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        base.Configure(builder);

        builder.ToTable("skills");

        builder
            .Property(p => p.Type)
            .HasConversion<EnumStringSnakeCaseConverter<SkillType>>()
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
            .Property("_tags")
            .HasColumnType("text[]")
            .HasColumnName("tags");
    }
}

public sealed class CompanySectorMapping : EntityMapping<CompanySector>
{
    public override void Configure(EntityTypeBuilder<CompanySector> builder)
    {
        base.Configure(builder);

        builder.ToTable("company_sectors");

        builder
            .Property(p => p.Name)
            .HasMaxLength(300)
            .IsRequired();
    }
}
