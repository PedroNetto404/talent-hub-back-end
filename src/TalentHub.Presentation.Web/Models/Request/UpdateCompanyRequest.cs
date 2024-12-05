using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Update;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record UpdateCompanyRequest
{
    [Required]
    [DeniedValues("00000000-0000-0000-0000-000000000000")]
    [FromRoute(Name = "companyId")]
    public required Guid Id { get; init; }

    [StringLength(100, MinimumLength = 3)]
    [Required]
    public required string LegalName { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string TradeName { get; init; }

    [StringLength(14, MinimumLength = 14)]
    [Required]
    public required string Cnpj { get; init; }

    [EmailAddress]
    [Required]
    public required string RecruitmentEmail { get; init; }

    [DeniedValues("00000000-0000-0000-0000-000000000000")]
    [Required]
    public required Guid SectorId { get; init; }

    [StringLength(11, MinimumLength = 11)]
    public string? Phone { get; init; }

    [Required]
    public required bool AutoMatchEnabled { get; init; }

    [Range(0, int.MaxValue)]
    [Required]
    public required int EmployeeCount { get; init; }

    [Url]
    [StringLength(2000)]
    public string? SiteUrl { get; init; }

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

    [Range(1900, 2100)]
    [Required]
    public required int FoundantionYear { get; init; }

    public UpdateCompanyCommand ToCommand() =>
        new(
            Id,
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
            FoundantionYear
        );
}
