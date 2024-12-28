using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetAll;

public sealed class GetAllCompaniesEndpoint :
    Ep.Req<GetAllCompaniesRequest>
      .Res<PageResponse<CompanyDto>>
{
    public override void Configure()
    {
        Get("");
        RequestBinder(new GetAllCompaniesRequestBinder());
        Validator<GetAllCompaniesRequestValidator>();
        Version(1);
        Group<CompanyEndpointGroup>();

        Roles(Role.Admin, Role.Company);
        Permissions(
            Permission.ReadAllCompanies
        );
    }

    public override Task HandleAsync(
        GetAllCompaniesRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new GetAllCompaniesQuery(
                req.NameLike,
                req.HasJobOpening,
                req.SectorIds ?? [],
                req.LocationLike,
                req.Limit!.Value,
                req.Offset!.Value,
                req.SortBy,
                req.SortOrder!.Value
                ),
                ct
    );
}
