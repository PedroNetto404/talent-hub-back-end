using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.RemoveGaleryItem;

public sealed record RemoveCompanyGaleryItemRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId,
    [property: FromBody] string Url
);
