using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.Update;

public sealed class UpdateCompanyEndpoint :
    Ep.Req<UpdateCompanyRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Put("{companyId:guid}");
        Version(1);
        Validator<UpdateCompanyRequestValidator>();
        Group<CompanyEndpointGroup>();
        Description(b =>
            b.Accepts<UpdateCompanyRequest>()
            .Produces<CompanyDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override  Task HandleAsync(UpdateCompanyRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new UpdateCompanyCommand(
                req.CompanyId,
                req.LegalName,
                    req.TradeName,
                    req.Cnpj,
                    req.About,
                    req.SectorId,
                    req.RecruitmentEmail,
                    req.Phone,
                    req.AutoMatchEnabled,
                    req.EmployeeCount,
                    req.SiteUrl,
                    req.Address,
                    req.InstagramUrl,
                    req.LinkedinUrl,
                    req.CareerPageUrl,
                    req.Mission,
                    req.Vision,
                    req.Values,
                    req.FoundationYear
            ),
            ct
        );
}
