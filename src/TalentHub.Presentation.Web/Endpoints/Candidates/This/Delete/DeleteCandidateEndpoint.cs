using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Delete;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.Delete;

public sealed class DeleteCandidateEndpoint
    : Ep.Req<DeleteCandidateRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{candidateId:guid}");
        Version(1);
        Group<CandidatesEndpointsGroup>();
        Validator<DeleteCandidateRequestValidator>();
        Roles(nameof(Role.Admin).Underscore());
    }

    public override Task HandleAsync(
        DeleteCandidateRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new DeleteCandidateCommand(req.CandidateId),
        ct
    );
}
