using FastEndpoints;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.GetById;

public sealed class GetCompanySectorByIdEndpoint :
    Ep.Req<GetCompanySectorByIdRequest>.Res<CompanySectorDto>
{
    public override void Configure()
    {
        Get("{companySectorId}");
        Version(1);
        Validator<GetCompanySectorByIdRequestValidator>();
        Group<CompanySectorsGroup>();
    }

    public override Task HandleAsync(GetCompanySectorByIdRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetCompanySectorByIdQuery(req.Id),
            ct            
        );
}

