using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Create;

public sealed record CreateUniversityRequest(
    [property: FromBody] string Name,
    [property: FromBody] string SiteUrl
);
