using Microsoft.AspNetCore.Mvc;
using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetAll;

public sealed record GetAllCoursesRequest(
    [property: FromQuery(Name = "name_like")] string? NameLike,
    [property: FromQuery(Name = "related_skill_id_in")] IEnumerable<Guid>? RelatedSkillIds
) : PageRequest;
