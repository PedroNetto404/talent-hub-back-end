using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed record GetCandidatesRequest(
    IEnumerable<Guid>? SkillIds,
    IEnumerable<string>? Languages
) : PageRequest;
