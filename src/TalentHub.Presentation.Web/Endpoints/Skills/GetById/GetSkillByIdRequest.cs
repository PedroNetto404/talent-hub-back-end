using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Skills.GetById;

public sealed record GetSkillByIdRequest(
    [property: FromRoute(Name = "skillId")] Guid SkillId
);