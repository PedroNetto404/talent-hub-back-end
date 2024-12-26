using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdatePresentationVideo;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdatePresentationVideo;

public sealed class UpdatePresentationVideoEndpoint :
    Ep.Req<UpdatePresentationVideoRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Patch("{companyId:guid}/presentation-video");
        Version(1);
        Group<CompanyEndpointGroup>();
        Validator<UpdatePresentationVideoRequestValidator>();
        Description(ep =>
            ep.Accepts<UpdatePresentationVideoRequest>()
            .Produces<CompanyDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override async Task HandleAsync(UpdatePresentationVideoRequest req, CancellationToken ct) 
    {
        await using MemoryStream ms = new();
        await req.File.CopyToAsync(ms, ct);

        await this.HandleUseCaseAsync(
            new UpdateCompanyPresentationVideoCommand(
                req.CompanyId,
                ms,
                req.File.ContentType
            ),
            ct
        );
    }
}

