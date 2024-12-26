using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Skills.Enums;
using TalentHub.Presentation.Web.Shared.RequestModels;

public sealed record GetAllSkillsRequest(
    [property: FromQuery(Name = "skill_id_in")] IEnumerable<Guid>? Ids,
    [property: FromQuery(Name = "type")] SkillType? Type
) : PageRequest;
