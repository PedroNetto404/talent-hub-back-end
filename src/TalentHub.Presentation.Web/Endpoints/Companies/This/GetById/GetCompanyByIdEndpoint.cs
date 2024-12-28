using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetById;

public sealed class GetCompanyByIdEndpoint :
    Ep.Req<GetCompanyByIdRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Get("{companyId}");
        Version(1);
        Group<CompanyEndpointGroup>();
    }

    public override Task HandleAsync(
        GetCompanyByIdRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new GetCompanyByIdQuery(CompanyId: req.CompanyId),
        ct
    );
}
