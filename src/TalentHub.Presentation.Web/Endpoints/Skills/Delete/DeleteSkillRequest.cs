using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Skills.Delete;

public sealed record DeleteSkillRequest(
    [property: FromRoute(Name = "skillId")]
    Guid SkillId
);
