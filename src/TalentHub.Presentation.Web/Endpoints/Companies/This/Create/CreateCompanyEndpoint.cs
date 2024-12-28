using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.Create;

public sealed class CreateCompanyEndpoint :
    Ep.Req<CreateCompanyRequest>.Res<CompanyDto>
{
    public override void Configure()
    {
        Post("");
        Version(1);
        Validator<CreateCompanyRequestValidator>();
        Group<CompanyEndpointGroup>();
    }

    public override Task HandleAsync(
        CreateCompanyRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new CreateCompanyCommand(
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
                ), ct
    );
}
