using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.LanguageProficiences.Delete;

public sealed class DeleteLanguageProficiencyRequest
{
    [FromRoute(Name = "candidateId")]
    public Guid CandidateId { get; init; }

    [FromRoute(Name = "languageProficiencyId")]
    public Guid LanguageProficiencyId { get; init; }
}
