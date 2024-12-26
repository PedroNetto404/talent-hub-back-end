using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetById;

public sealed class GetCandidateByIdEndpoint :
    Ep.Req<GetCandidateByIdRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Get("{candidateId:guid}");

        Description(ep =>
            ep.Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status200OK));

        Group<CandidatesEndpointsGroup>();
        Validator<GetCandidateByIdRequestValidator>();
        Version(1);
    }

    public override Task HandleAsync(
        GetCandidateByIdRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new GetCandidateByIdQuery(req.CandidateId),
        ct);
}
