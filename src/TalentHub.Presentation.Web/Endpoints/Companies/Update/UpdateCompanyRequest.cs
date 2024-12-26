using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Companies.Update;

public sealed record UpdateCompanyRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    [property: FromBody] string LegalName,
    [property: FromBody] string TradeName,
    [property: FromBody] string Cnpj,
    [property: FromBody] string? About,
    [property: FromBody] Guid SectorId,
    [property: FromBody] string RecruitmentEmail,
    [property: FromBody] string? Phone,
    [property: FromBody] bool AutoMatchEnabled,
    [property: FromBody] int EmployeeCount,
    [property: FromBody] string? SiteUrl,
    [property: FromBody] Address Address,
    [property: FromBody] string? InstagramUrl,
    [property: FromBody] string? LinkedinUrl,
    [property: FromBody] string? CareerPageUrl,
    [property: FromBody] string? Mission,
    [property: FromBody] string? Vision,
    [property: FromBody] string? Values,
    [property: FromBody] int FoundationYear
);
