using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Universities;

namespace TalentHub.Infra.Data.Mappings;

public sealed class EducationalInstituteMapping : EntityMapping<University>
{
    public override void Configure(EntityTypeBuilder<University> builder)
    {
        base.Configure(builder);

        builder.ToTable("universities");

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
            .Property(p => p.SiteUrl)
            .HasMaxLength(500);
    }
}
