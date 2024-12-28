using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.UpdatePresentationVideo;

public sealed record UpdatePresentationVideoRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    IFormFile File
);
