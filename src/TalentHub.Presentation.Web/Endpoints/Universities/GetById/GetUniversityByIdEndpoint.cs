using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Universities.GetById;

public sealed class GetUniversityByIdEndpoint :
    Ep.Req<GetUniversityByIdRequest>.Res<UniversityDto>
{
    public override void Configure()
    {
        Get("{universityId:guid}");
        Group<UniversitiesGroup>();
        Version(1);
        Description(b =>
            b.Accepts<GetUniversityByIdRequest>()
            .Produces<UniversityDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
        );
        Validator<GetUniversityByIdRequestValidator>();
        Definition.AllowedRoles?.Clear();
    }

    public override Task HandleAsync(GetUniversityByIdRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetUniversityByIdQuery(req.UniversityId),
            ct
        );
}
