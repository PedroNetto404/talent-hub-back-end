using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Universities.GetById;

public sealed record GetUniversityByIdRequest(
    [property: FromRoute(Name = "universityId")] Guid UniversityId
);
