using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Certificates;

public sealed class CandidateCertificatesEndpointsSubGroup : SubGroup<CandidatesEndpointsGroup>
{
    public CandidateCertificatesEndpointsSubGroup()
    {
        Configure("{candidateId:guid}/certificates", static ep =>
        {
        });
    }
}
