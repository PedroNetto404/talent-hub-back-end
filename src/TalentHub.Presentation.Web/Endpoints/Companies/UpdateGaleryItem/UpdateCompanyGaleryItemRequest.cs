using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdateGaleryItem;

public sealed record UpdateCompanyGaleryItemRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    IFormFile File
);
