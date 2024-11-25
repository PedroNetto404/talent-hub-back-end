using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Candidates;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CandidateMapping : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("candidates");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .HasIndex(p => p.Email)
            .IsUnique();

        builder
            .Property(c => c.Phone)
            .IsRequired()
            .HasMaxLength(11);

        builder
            .HasIndex(c => c.Phone)
            .IsUnique();

        builder.OwnsOne(c => c.Address, q =>
        {
            q
            .Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(200);

            q
            .Property(a => a.ZipCode)
            .IsRequired()
            .HasMaxLength(8);

            q
            .Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(20);

            q
            .Property(a => a.Number)
            .IsRequired()
            .HasMaxLength(15);

            q
            .Property(a => a.Neighborhood)
            .IsRequired()
            .HasMaxLength(50);

            q
            .Property(a => a.State)
            .IsRequired()
            .HasMaxLength(20);
        });

        builder.Ignore(c => c.Age);

        builder
            .Property(p => p.BirthDate)
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(c => c.Summary)
            .HasMaxLength(200);

        builder
            .Property(c => c.ExpectedRemuneration)
            .HasPrecision(12, 2);

        builder
            .Metadata
            .FindNavigation(nameof(Candidate.Skills))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Metadata
            .FindNavigation(nameof(Candidate.Experiences))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Metadata
            .FindNavigation(nameof(Candidate.LanguageProficiencies))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Property("_hobbies")
            .HasColumnName("hobbies")
            .HasColumnType("text[]");

        builder
            .Property("_desiredJobTypes")
            .HasColumnType("text[]")
            .HasColumnName("desired_job_types")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Property("_desiredWorkplaceTypes")
            .HasColumnType("text[]")
            .HasColumnName("desired_workplace_types")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}