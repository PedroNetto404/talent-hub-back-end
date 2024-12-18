using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentHub.ApplicationCore.Resources.Companies;
using TalentHub.ApplicationCore.Resources.CompanySectors;

namespace TalentHub.Infra.Data.Mappings;

public sealed class CompanyMapping : AuditableAggregateRootMapping<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.ToTable("companies");

        builder.Property(p => p.LegalName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.TradeName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Cnpj)
            .HasMaxLength(14)
            .IsRequired();

        builder.HasIndex(p => p.Cnpj)
            .IsUnique();

        builder.Property(p => p.RecruitmentEmail)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(p => p.RecruitmentEmail)
            .IsUnique();

        builder.Property(p => p.Phone)
            .HasMaxLength(11);

        builder.HasIndex(p => p.Phone)
            .IsUnique();

        builder.Property(p => p.AutoMatchEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.EmployeeCount)
            .IsRequired();

        builder.Property(p => p.SiteUrl)
            .HasMaxLength(500);

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

        builder.Property(p => p.About)
            .HasMaxLength(500);

        builder.Property(p => p.InstagramUrl);

        builder.Property(p => p.CareerPageUrl);

        builder.Property(p => p.PresentationVideoUrl);

        builder.Property(p => p.Mission);

        builder.Property(p => p.Vision);

        builder.Property(p => p.Values);

        builder.Property(p => p.FoundationYear);

        builder.Property("_galery")
            .HasColumnType("text[]")
            .HasColumnName("galery");

        builder.HasOne<CompanySector>()
            .WithMany()
            .HasForeignKey(p => p.SectorId);
    }
}
