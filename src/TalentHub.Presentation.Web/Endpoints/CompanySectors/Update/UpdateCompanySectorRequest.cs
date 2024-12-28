using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.Update;

public sealed record UpdateCompanySectorRequest(
    [property: FromRoute(Name = "companySectorId")] Guid Id,
    [property: FromBody] string Name
);
