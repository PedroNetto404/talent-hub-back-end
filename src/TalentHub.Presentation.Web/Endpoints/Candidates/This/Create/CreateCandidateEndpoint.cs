using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.Create;

public sealed class CreateCandidateEndpoint : Ep.Req<CreateCandidateRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");
        Description(builder => builder.Accepts<CreateCandidateRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
        );
        Validator<CreateCandidateRequestValidator>();
        Version(1);
        Group<CandidatesEndpointsGroup>();
    }

    public override Task HandleAsync(CreateCandidateRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new CreateCandidateCommand(
                req.Name,
                req.AutoMatchEnabled,
                req.Phone,
                req.BirthDate,
                req.Address,
                req.DesiredJobTypes,
                req.DesiredWorkplaceTypes,
                req.Summary,
                req.GitHubUrl,
                req.InstagramUrl,
                req.LinkedInUrl,
                req.ExpectedRemuneration,
                req.Hobbies
            ),
            ct,
            onSuccessCallback: (dto) => Results.Created($"/api/v1/candidates/{dto.Id}", dto)
        );
}
