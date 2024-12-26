using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Experiences;

public sealed class ExperiencesSubGroup : SubGroup<CandidatesEndpointsGroup>
{
    public ExperiencesSubGroup()
    {
        Configure("{candidateId:guid}/experiences", static b => { });
    }
}
