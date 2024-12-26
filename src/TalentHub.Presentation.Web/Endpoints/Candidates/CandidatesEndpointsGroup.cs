using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates;

public sealed class CandidatesEndpointsGroup : Group
{
    public CandidatesEndpointsGroup()
    {
        Configure("candidates", static ep =>
        {
        });
    }
}
