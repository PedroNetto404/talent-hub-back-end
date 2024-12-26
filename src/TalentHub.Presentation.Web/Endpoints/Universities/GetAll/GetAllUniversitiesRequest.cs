using Microsoft.AspNetCore.Mvc;
using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Universities.GetAll;

public sealed record GetAllUniversitiesRequest(
    [property: FromQuery(Name = "name_like")] string? NameLike
) : PageRequest;
