using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Delete;
using TalentHub.Presentation.Web.Extensions;

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

    public override Task HandleAsync(
        DeleteLanguageProficiencyRequest req,
        CancellationToken ct
    ) =>
        this.HandleUseCaseAsync(
            new DeleteLanguageProficiencyCommand(
                req.CandidateId,
                req.LanguageProficiencyId
            ),
            ct);
}
