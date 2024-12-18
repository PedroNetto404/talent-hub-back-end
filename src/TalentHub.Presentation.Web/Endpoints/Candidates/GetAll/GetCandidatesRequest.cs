using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed record GetCandidatesRequest : PageRequest
{
    public IEnumerable<Guid> SkillIds { get; init; } = [];

    public IEnumerable<string> Languages { get; init; } = [];
}
