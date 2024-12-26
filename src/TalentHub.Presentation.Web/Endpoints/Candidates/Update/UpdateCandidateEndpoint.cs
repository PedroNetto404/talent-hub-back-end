using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Update;

public sealed class UpdateCandidateEndpoint : Ep.Req<UpdateCandidateRequest>.NoRes
{
    public override void Configure()
    {
        Put("{candidateId:guid}");
        Validator<UpdateCandidateRequestValidator>();
        Group<CandidatesEndpointsGroup>();

        Description(d =>
            d.Accepts<UpdateCandidateRequest>()
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
        );

        Version(1);
    }

    public override Task HandleAsync(UpdateCandidateRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(new UpdateCandidateCommand(
            req.CandidateId,
            req.Name,
            req.AutoMatchEnabled,
            req.Phone,
            req.Address,
            req.DesiredWorkplaceTypes,
            req.DesiredJobTypes,
            req.ExpectedRemuneration,
            req.InstagramUrl,
            req.LinkedInUrl,
            req.GitHubUrl,
            req.Summary,
            req.Hobbies
        ), ct);
}
