using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemovePresentationVideo;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.RemovePresentationVideo;

public sealed record RemoveCompanyPresentationVideoRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId
);

public sealed class RemoveCompanyPresentationVideoEndpoint
    : Ep.Req<RemoveCompanyPresentationVideoRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Patch("{companyId:guid}/presentation-video");
        Version(1);
        Group<CompanyEndpointGroup>();
        Validator<RemoveCompanyPresentationVideoRequestValidator>();
        Description(b =>
            b.Accepts<RemoveCompanyPresentationVideoRequest>()
                .Produces<CompanyDto>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
        );
    }

    public override Task HandleAsync(
        RemoveCompanyPresentationVideoRequest req,
        CancellationToken ct
    ) =>
        this.HandleUseCaseAsync(
            new RemoveCompanyPresentationVideoCommand(req.CompanyId),
            ct
        );
}
