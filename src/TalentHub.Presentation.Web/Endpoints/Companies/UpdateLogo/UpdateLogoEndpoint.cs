using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdateLogo;

public sealed record UpdateCompanyLogoRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    [property: FromForm(Name = "file")] IFormFile File
);
