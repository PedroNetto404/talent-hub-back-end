using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Companies.Create;

public sealed record CreateCompanyRequest(
    string LegalName,
    string TradeName,
    string Cnpj,
    string? About,
    Guid SectorId,
    string RecruitmentEmail,
    string? Phone,
    bool AutoMatchEnabled,
    int EmployeeCount,
    string? SiteUrl,
    Address Address,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? CareerPageUrl,
    string? Mission,
    string? Vision,
    string? Values,
    int FoundationYear
);
