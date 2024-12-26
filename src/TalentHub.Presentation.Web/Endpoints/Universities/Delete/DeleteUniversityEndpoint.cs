using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Delete;

public sealed class DeleteUniversityEndpoint :
    Ep.Req<DeleteUniversityRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{universityId:guid}");
        Group<UniversitiesGroup>();
        Version(1);
        Description(b =>
            b.Accepts<DeleteUniversityRequest>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
        );
        Validator<DeleteUniversityRequestValidator>();
    }

    public override Task HandleAsync(DeleteUniversityRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new DeleteUniversityCommand(req.UniversityId),
            ct
        );
}
