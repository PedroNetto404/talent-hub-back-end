using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates;

public sealed class CandidateCertificatesEndpointsSubGroup : SubGroup<CandidatesEndpointsGroup>
{
    public CandidateCertificatesEndpointsSubGroup()
    {
        Configure("{candidateId:guid}/certificates", static ep =>
        {
        });
    }
}
