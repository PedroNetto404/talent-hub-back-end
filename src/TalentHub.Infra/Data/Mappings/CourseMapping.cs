using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Courses;
using TalentHub.ApplicationCore.Resources.Skills;
using TalentHub.Infra.Data.Mappings.Abstractions;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CourseMapping : EntityMapping<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);

        builder.ToTable("courses");

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
            .Property("_tags")
            .HasColumnName("tags")
            .HasColumnType("text[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany<Skill>()
            .WithMany()
            .UsingEntity("related_course_skills");
    }
}
