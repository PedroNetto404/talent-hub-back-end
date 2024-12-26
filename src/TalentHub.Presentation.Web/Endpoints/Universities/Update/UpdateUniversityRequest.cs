using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Update;

public sealed record UpdateUniversityRequest(
    [property: FromRoute(Name = "universityId")] Guid UniversityId,
    [property: FromBody] string Name,
    [property: FromBody] string SiteUrl
);
