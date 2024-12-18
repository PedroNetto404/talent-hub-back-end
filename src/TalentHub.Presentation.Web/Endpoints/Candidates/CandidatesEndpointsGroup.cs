using System;
using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Candidates;

public sealed class CandidatesEndpointsGroup : Group
{
    public CandidatesEndpointsGroup()
    {
        Configure("candidates", static ep =>
            ep.Description(static b =>
                b.WithDisplayName("Candidates Resource"))
        );
    }
}
