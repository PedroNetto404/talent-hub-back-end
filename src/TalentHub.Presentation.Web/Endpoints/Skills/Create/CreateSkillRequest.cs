
using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Create;

public sealed record CreateSkillRequest(
    [property: FromBody] string Name,
    [property: FromBody] string Type,
    [property: FromBody] IEnumerable<string>? Tags
);
