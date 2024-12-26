using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.Create;

public sealed record CreateCompanySectorRequest(
    [property: FromBody] string Name
);
