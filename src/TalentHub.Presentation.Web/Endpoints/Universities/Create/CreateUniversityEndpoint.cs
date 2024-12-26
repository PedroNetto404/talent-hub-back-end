using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Create;

public sealed class CreateUniversityEndpoint : Ep.Req<CreateUniversityRequest>.Res<UniversityDto>
{
    public override void Configure()
    {
        Post("");
        Group<UniversitiesGroup>();
        Version(1);
        Validator<CreateUniversityRequestValidator>();
        Description(b =>
            b.Accepts<CreateUniversityRequest>()
            .Produces<UniversityDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
        );
    }

    public override Task HandleAsync(CreateUniversityRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new CreateUniversityCommand(req.Name, req.SiteUrl),
            ct
        );
}
