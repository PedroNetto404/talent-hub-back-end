using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Delete;

public sealed record DeleteUniversityRequest(
    [property: FromRoute(Name = "universityId")] Guid UniversityId
);
