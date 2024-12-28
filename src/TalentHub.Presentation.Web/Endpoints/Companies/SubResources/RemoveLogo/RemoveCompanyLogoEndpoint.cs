using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveLogo;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.RemoveLogo;

public sealed class RemoveCompanyLogoEndpoint :
    Ep.Req<RemoveCompanyLogoRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Delete("{companyId:guid}/logo");
        Version(1);
        Group<CompanyEndpointGroup>();
        Validator<RemoveCompanyLogoRequestValidator>();
        Description(b =>
            b.Accepts<RemoveCompanyLogoRequest>()
                .Produces<CompanyDto>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
        );
    }

    public override Task HandleAsync(
        RemoveCompanyLogoRequest req,
        CancellationToken ct
    ) =>
        this.HandleUseCaseAsync(
            new RemoveCompanyLogoCommand(req.CompanyId),
            ct
        );
}
