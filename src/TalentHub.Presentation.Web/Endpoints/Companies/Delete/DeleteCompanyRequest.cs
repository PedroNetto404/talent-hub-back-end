using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.Delete;

public sealed record DeleteCompanyRequest(
    [property: FromRoute(Name = "companyId")] Guid CompanyId
);

public sealed class DeleteCompanyEndpoint :
    Ep.Req<DeleteCompanyRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{companyId:guid}");
        Version(1);
        Validator<DeleteCompanyRequestValidator>();
        Group<CompanyEndpointGroup>();
        Description(b =>
            b.Accepts<DeleteCompanyRequest>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override Task HandleAsync(DeleteCompanyRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new DeleteCompanyCommand(req.CompanyId),
            ct
        );
}
