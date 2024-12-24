using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Create;

public sealed class CreateCandidateEndpoint : Ep.Req<CreateCandidateRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");

        Description(builder => builder.Accepts<CreateCandidateRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Create a new candidate.")
            .WithDisplayName("Create Candidate")
        );

        Validator<CreateCandidateRequestValidator>();
        Version(1);
        Group<CandidatesEndpointsGroup>();
    }

    public override async Task HandleAsync(CreateCandidateRequest req, CancellationToken ct) => 
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
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
                    ct
                ),
                onSuccess: (dto) => Results.Created($"/api/v1/candidates/{dto.Id}", dto) 
            )
        );
}
