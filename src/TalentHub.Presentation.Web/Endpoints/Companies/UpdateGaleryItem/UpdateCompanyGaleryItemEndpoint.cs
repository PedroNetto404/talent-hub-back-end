using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Update;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateGaleryItem;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdateGaleryItem;

public sealed class UpdateCompanyGaleryItemEndpoint :
    Ep.Req<UpdateCompanyGaleryItemRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Patch("{companyId:guid}/galery");
        Version(1);
        Group<CompanyEndpointGroup>();
        Validator<UpdateCompanyGaleryItemRequestValidator>();
        Description(b =>
            b.Accepts<UpdateCompanyGaleryItemRequest>()
                .Produces<CompanyDto>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
        );
    }

    public override async Task HandleAsync(
        UpdateCompanyGaleryItemRequest req,
        CancellationToken ct
    ) 
    {
        await using MemoryStream ms = new();
        await req.File.CopyToAsync(ms, ct);

        await this.HandleUseCaseAsync(
            new UpdateCompanyGaleryCommand(
                req.CompanyId,
                ms,
                req.File.ContentType
            ),
            ct
        );
    }
}
