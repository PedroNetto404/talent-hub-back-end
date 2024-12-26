using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Update;

public sealed class UpdateUniversityEndpoint :
    Ep.Req<UpdateUniversityRequest>.Res<UniversityDto>
{
    public override void Configure()
    {
        Put("{universityId:guid}");
        Group<UniversitiesGroup>();
        Version(1);
        Validator<UpdateUniversityRequestValidator>();
        Description(b =>
            b.Accepts<UpdateUniversityRequest>()
            .Produces<UniversityDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
        );
    }

    public override Task HandleAsync(UpdateUniversityRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new UpdateUniversityCommand(req.UniversityId, req.Name, req.SiteUrl),
            ct
        );
}
