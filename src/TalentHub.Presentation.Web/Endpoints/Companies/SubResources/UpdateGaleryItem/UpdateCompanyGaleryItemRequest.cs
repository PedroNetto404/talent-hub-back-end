using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.UpdateGaleryItem;

public sealed record UpdateCompanyGaleryItemRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    IFormFile File
);
