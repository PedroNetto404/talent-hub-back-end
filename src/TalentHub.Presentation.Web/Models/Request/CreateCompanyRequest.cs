using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Create;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record CreateCompanyRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string LegalName { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string TradeName { get; init; }

    [Required]
    [StringLength(14, MinimumLength = 14)]
    public required string Cnpj { get; init; }

    [Required]
    [EmailAddress]
    public required string RecruitmentEmail { get; init; }

    [Required]
    [DeniedValues("00000000-0000-0000-0000-000000000000")]
    public required Guid SectorId { get; init; }

    [StringLength(11, MinimumLength = 11)]
    public string? Phone { get; init; }

    public bool AutoMatchEnabled { get; init; } = true;

    [Range(0, int.MaxValue)]
    public int EmployeeCount { get; init; }

    [Url]
    [StringLength(2000)]
    public string? SiteUrl { get; init; }

    [Required]
    public required AddressRequest Address { get; init; }

    [StringLength(500)]
    public string? About { get; init; }

    [Url]
    [StringLength(2000)]
    public string? InstagramUrl { get; init; }

    [Url]
    [StringLength(2000)]
    public string? FacebookUrl { get; init; }

    [Url]
    [StringLength(2000)]
    public string? LinkedinUrl { get; init; }

    [Url]
    [StringLength(2000)]
    public string? CareerPageUrl { get; init; }

    [StringLength(500)]
    public string? Mission { get; init; }

    [StringLength(500)]
    public string? Vision { get; init; }

    [StringLength(500)]
    public string? Values { get; init; }

    [Required]
    [Range(1900, 2100)]
    public required int FoundationYear { get; init; }

    public CreateCompanyCommand ToCommand() =>
        new(
            LegalName,
            TradeName,
            Cnpj,
            About,
            SectorId,
            RecruitmentEmail,
            Phone,
            AutoMatchEnabled,
            EmployeeCount,
            SiteUrl,
            Address.ToValueObject(),
            InstagramUrl,
            FacebookUrl,
            LinkedinUrl,
            CareerPageUrl,
            Mission,
            Vision,
            Values,
            FoundationYear
        );
}
