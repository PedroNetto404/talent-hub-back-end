using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.RemoveLogo;

public sealed record RemoveCompanyLogoRequest(
    [property: FromRoute] Guid CompanyId
);
