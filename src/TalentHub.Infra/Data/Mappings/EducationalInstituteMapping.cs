using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.EducationalInstitutes;

namespace TalentHub.Infra.Data.Mappings;

public sealed class EducationalInstituteMapping : IEntityTypeConfiguration<EducationalInstitute>
{
    public void Configure(EntityTypeBuilder<EducationalInstitute> builder)
    {
        builder.ToTable("educational_institutes");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder
            .Property(p => p.Name)
            .IsRequired();
    }
}