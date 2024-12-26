using Microsoft.AspNetCore.Mvc;

public sealed record GetSkillByIdRequest(
    [property: FromRoute(Name = "skillId")] Guid SkillId
);

