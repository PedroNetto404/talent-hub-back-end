using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdatePresentationVideo;

public sealed record UpdatePresentationVideoRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    IFormFile File
);
