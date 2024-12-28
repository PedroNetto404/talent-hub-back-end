using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.GetById;

public sealed class GetCandidateByIdEndpoint :
    Ep.Req<GetCandidateByIdRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Get("{candidateId:guid}");
        Group<CandidatesEndpointsGroup>();
        Validator<GetCandidateByIdRequestValidator>();
        Version(1);

        Description(ep =>
        ep.Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK));

        Permissions(Permission.ReadCandidateById);
    }

    public override Task HandleAsync(
        GetCandidateByIdRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new GetCandidateByIdQuery(req.CandidateId),
        ct);
}
