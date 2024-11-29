using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Candidates;
using TalentHub.ApplicationCore.Users;
using TalentHub.ApplicationCore.Users.Enums;
using TalentHub.Infra.Data.ValueConverters;

namespace TalentHub.Infra.Data.Mappings;

public sealed class UserMapping : AuditableAggregateRootMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("users");

        builder
            .Property(p => p.Email)
            .HasMaxLength(200);
        builder
            .HasIndex(p => p.Email)
            .IsUnique();

        builder
            .Property(p => p.HashedPassword)
            .HasMaxLength(500)
            .IsRequired();

        builder.OwnsOne(p => p.RefreshToken, tokenBuilder =>
        {
            tokenBuilder.Property(p => p.Value).HasColumnName("refresh_token_value");
            tokenBuilder.Property(p => p.Expiration).HasColumnName("refresh_token_expiration");
        });

        builder
            .Property(p => p.Role)
            .HasConversion<EnumStringSnakeCaseConverter<Role>>()
            .IsRequired();

        builder
            .HasOne<Candidate>()
            .WithOne()
            .HasForeignKey<Candidate>(c => c.Id);
    }
}
