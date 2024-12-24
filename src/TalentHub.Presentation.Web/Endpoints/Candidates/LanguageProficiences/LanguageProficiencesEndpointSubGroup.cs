using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences;

public sealed class LanguageProficiencesEndpointSubGroup : SubGroup<CandidatesEndpointsGroup>
{
    public LanguageProficiencesEndpointSubGroup() => 
        Configure("{candidateId:guid}/language-proficiences", static ep => { });
}