using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Delete;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Delete;

public sealed class DeleteLanguageProficiencyEndpoint :
    Ep.Req<DeleteLanguageProficiencyRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{languageProficiencyId:guid}");
        Description(b =>
            b.Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
        );
        Version(1);
        Group<LanguageProficiencesEndpointSubGroup>();
        Validator<DeleteLanguageProficiencyRequestValidator>();
    }

    public override async Task HandleAsync(
        DeleteLanguageProficiencyRequest req,
        CancellationToken ct) =>
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new DeleteLanguageProficiencyCommand(
                        req.CandidateId,
                        req.LanguageProficiencyId
                    ),
                    ct
                )
            )
        );
}
