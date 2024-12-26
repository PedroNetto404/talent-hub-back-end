using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Companies.Dtos;

public sealed record CompanyDto(
    Guid Id,
    string LegalName,
    string TradeName,
    string Cnpj,
    string RecruitmentEmail,
    string? Phone,
    bool AutoMatchEnabled,
    int EmployeeCount,
    string? LogoUrl,
    string? SiteUrl,
    Address Address,
    string? About,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? CareerPageUrl,
    string? PresentationVideoUrl,
    string? Mission,
    string? Vision,
    string? Values,
    int FoundantionYear,
    IReadOnlyList<string> Galery
)
{
    public static CompanyDto FromEntity(Company entity) =>
        new(
            entity.Id,
            entity.LegalName,
            entity.TradeName,
            entity.Cnpj,
            entity.RecruitmentEmail,
            entity.Phone,
            entity.AutoMatchEnabled,
            entity.EmployeeCount,
            entity.LogoUrl,
            entity.SiteUrl,
            entity.Address,
            entity.About,
            entity.InstagramUrl,
            entity.LinkedinUrl,
            entity.CareerPageUrl,
            entity.PresentationVideoUrl,
            entity.Mission,
            entity.Vision,
            entity.Values,
            entity.FoundationYear,
            entity.Galery
        );
}
