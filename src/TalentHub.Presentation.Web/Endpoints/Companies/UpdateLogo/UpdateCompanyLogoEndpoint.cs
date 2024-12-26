using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateLogo;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdateLogo;

public sealed class UpdateCompanyLogoEndpoint :
    Ep.Req<UpdateCompanyLogoRequest>
      .Res<CompanyDto>
{
    public override void Configure()
    {
        Patch("{companyId:guid}/logo");
        Version(1);
        Validator<UpdateCompanyLogoRequestValidator>();
        Group<CompanyEndpointGroup>();
        Description(b =>
            b.Accepts<UpdateCompanyLogoRequest>()
            .Produces<CompanyDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override async Task HandleAsync(UpdateCompanyLogoRequest req, CancellationToken ct)
    {
        await using MemoryStream ms = new();
        await req.File.CopyToAsync(ms, ct);

        await this.HandleUseCaseAsync(
            new UpdateCompanyLogoCommand(
                req.CompanyId,
                ms,
                 req.File.ContentType),
            ct
        );
    }
}
