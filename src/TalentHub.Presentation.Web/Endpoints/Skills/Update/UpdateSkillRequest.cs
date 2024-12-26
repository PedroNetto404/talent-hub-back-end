using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Update;

public sealed record UpdateSkillRequest(
    [property: FromRoute(Name = "skillId")] Guid SkillId,
    [property: FromBody] string Name,
    [property: FromBody] string Type,
    [property: FromBody] IEnumerable<string>? Tags
);
