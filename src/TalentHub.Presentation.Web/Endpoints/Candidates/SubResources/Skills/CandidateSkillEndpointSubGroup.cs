using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Skills;

public class CandidateSkillEndpointSubGroup : SubGroup<CandidatesEndpointsGroup>
{   
    public CandidateSkillEndpointSubGroup()
    {
        Configure("{candidateId:guid}/skills", static ep => { });
    }
}   
