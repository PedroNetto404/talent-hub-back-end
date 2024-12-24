using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.UpdateResume;

public record class UpdateResumeRequest
{
    [FromForm]
    public IFormFile File { get; init; }

    [FromRoute(Name = "candidateId")]
    public Guid CandidateId { get; init; }
}
