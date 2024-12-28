using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Candidates;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.Infra.Data.Mappings.Abstractions;

namespace TalentHub.Infra.Data.Mappings.CandidateAggregate;

public sealed class CandidateMapping : AuditableAggregateRootMapping<Candidate>
{
    public override void Configure(EntityTypeBuilder<Candidate> builder)
    {
        base.Configure(builder);

        builder
          .ToTable("candidates");
        builder
          .Property(c => c.Name)
          .IsRequired()
          .HasMaxLength(200);
        builder
          .Property(c => c.Phone)
          .IsRequired()
          .HasMaxLength(11);
        builder
          .HasIndex(c => c.Phone)
          .IsUnique();
        builder
          .OwnsOne(c => c.Address, q =>
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
        builder
          .Ignore(c => c.Age);
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
          .HasMany(p => p.Skills)
          .WithOne()
          .HasForeignKey("candidate_id")
          .OnDelete(DeleteBehavior.Cascade);

        builder
          .Metadata
          .FindNavigation(nameof(Candidate.Experiences))!
          .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder
          .HasMany(p => p.Experiences)
          .WithOne()
          .HasForeignKey("candidate_id")
          .OnDelete(DeleteBehavior.Cascade);

        builder
          .Metadata
          .FindNavigation(nameof(Candidate.LanguageProficiencies))!
          .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder
          .HasMany(p => p.LanguageProficiencies)
          .WithOne()
          .HasForeignKey("candidate_id")
          .OnDelete(DeleteBehavior.Cascade);

        builder
          .Metadata
          .FindNavigation(nameof(Candidate.Certificates))!
          .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder
          .HasMany(p => p.Certificates)
          .WithOne()
          .HasForeignKey("candidate_id")
          .OnDelete(DeleteBehavior.Cascade);

        builder
          .Property("_hobbies")
          .HasColumnName("hobbies")
          .HasColumnType("text[]");

        builder
          .Property<List<JobType>>("_desiredJobTypes")
          .HasColumnType("text[]")
          .HasColumnName("desired_job_types")
          .UsePropertyAccessMode(PropertyAccessMode.Field)
          .HasConversion(
              p => p.Select(q => q.ToString().Underscore()),
              p => p.Select(q => Enum.Parse<JobType>(q.Pascalize(), true)).ToList(),
              new ValueComparer<List<JobType>>(
                  (c1, c2) => c1!.SequenceEqual(c2!),
                  c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                  c => c.ToList()
              )
          );

        builder
          .Property<List<WorkplaceType>>("_desiredWorkplaceTypes")
          .HasColumnType("text[]")
          .HasColumnName("desired_workplace_types")
          .UsePropertyAccessMode(PropertyAccessMode.Field)
          .HasConversion(
              p => p.Select(q => q.ToString().Underscore()),
              p => p.Select(q => Enum.Parse<WorkplaceType>(q.Pascalize(), true)).ToList(),
              new ValueComparer<List<WorkplaceType>>(
                  (c1, c2) => c1!.SequenceEqual(c2!),
                  c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                  c => c.ToList()
              )
          );

        builder
          .HasOne<User>()
          .WithOne()
          .HasForeignKey<Candidate>(p => p.UserId)
          .OnDelete(DeleteBehavior.Cascade);
    }
}
