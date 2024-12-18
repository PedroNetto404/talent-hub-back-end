using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates;

public sealed class CandidateCertificatesEndpointsGroup : SubGroup<CandidatesEndpointsGroup>
{
    public CandidateCertificatesEndpointsGroup()
    {
        Configure("{candidateId:guid}/certificates", static ep =>
        {
        });
    }
}
