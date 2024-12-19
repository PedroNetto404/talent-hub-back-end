using Microsoft.AspNetCore.Mvc;
using TalentHub.Presentation.Web.Binders;
using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed record GetCandidatesRequest : PageRequest
{
    [ModelBinder(typeof(SplitQueryStringBinder))]
    [FromQuery(Name = "skill_id_in")]
    public IEnumerable<Guid> SkillIds { get; init; } = [];

    [ModelBinder(typeof(SplitQueryStringBinder))]
    [FromQuery(Name = "language_in")]
    public IEnumerable<string> Languages { get; init; } = [];
}
