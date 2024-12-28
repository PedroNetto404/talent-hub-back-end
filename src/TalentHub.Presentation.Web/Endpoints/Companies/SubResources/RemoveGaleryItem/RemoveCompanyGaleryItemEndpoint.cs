using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveGaleryItem;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.RemoveGaleryItem;

public sealed class RemoveCompanyGaleryItemEndpoint
    : Ep.Req<RemoveCompanyGaleryItemRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Delete("{companyId:guid}/galery");
        Version(1);
        Group<CompanyEndpointGroup>();
        Validator<RemoveCompanyGaleryItemRequestValidator>();
        Description(b =>
            b.Accepts<RemoveCompanyGaleryItemRequest>()
                .Produces<CompanyDto>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
        );
    }

    public override Task HandleAsync(
        RemoveCompanyGaleryItemRequest req,
        CancellationToken ct
    ) =>
        this.HandleUseCaseAsync(
            new RemoveCompanyGaleryItemCommand(req.CompanyId, req.Url),
            ct
        );
}
