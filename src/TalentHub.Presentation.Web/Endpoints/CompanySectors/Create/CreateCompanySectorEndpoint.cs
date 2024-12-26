using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors.Create;

public sealed class CreateCompanySectorEndpoint :
    Ep.Req<CreateCompanySectorRequest>.Res<CompanySectorDto>
{
    public override void Configure()
    {
        Post("");
        Group<CompanySectorsGroup>();
        Version(1);
        Validator<CreateCompanySectorRequestValidator>();
    }

    public override Task HandleAsync(CreateCompanySectorRequest req, CancellationToken ct) => 
        this.HandleUseCaseAsync(
            new CreateCompanySectorCommand(req.Name),
            ct
        );
}

