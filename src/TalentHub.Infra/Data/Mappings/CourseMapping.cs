using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.Skills;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CourseMapping : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

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
