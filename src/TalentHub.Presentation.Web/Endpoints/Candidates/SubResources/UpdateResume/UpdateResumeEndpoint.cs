using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateResume.Update;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.UpdateResume;

public sealed class UpdateResumeEndpoint :
    Ep.Req<UpdateResumeRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Patch("{candidateId:guid}/resume");
        AllowFileUploads();
        Version(1);
        Group<CandidatesEndpointsGroup>();
        Validator<UpdateResumeRequestValidator>();
        Description(b =>
            b.Accepts<UpdateResumeRequest>()
                .Produces<CandidateDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override async Task HandleAsync(UpdateResumeRequest req, CancellationToken ct)
    {
        await using var ms = new MemoryStream();
        await req.File.CopyToAsync(ms, ct);

        await this.HandleUseCaseAsync(
            new UpdateCandidateResumeCommand(
                req.CandidateId,
                ms
            ),
            ct);
    }
}
