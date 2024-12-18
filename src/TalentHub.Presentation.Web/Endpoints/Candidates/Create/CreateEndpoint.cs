using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Create;

public sealed class CreateEndpoint : Ep.Req<CreateCandidateRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");
        Group<CandidatesEndpointsGroup>();
        Validator<CreateCandidateRequestValidator>();
        Version(1);
    }

    public override async Task HandleAsync(CreateCandidateRequest req, CancellationToken ct)
    {
        CreateCandidateCommand command = new(
            req.Name,
            req.AutoMatchEnabled,
            req.Phone,
            req.BirthDate,
            req.AddressStreet,
            req.AddressNumber,
            req.AddressNeighborhood,
            req.AddressCity,
            req.AddressState,
            req.AddressCountry,
            req.AddressZipCode,
            req.DesiredJobTypes,
            req.DesiredWorkplaceTypes,
            req.Summary,
            req.GitHubUrl,
            req.InstagramUrl,
            req.LinkedInUrl,
            req.ExpectedRemuneration,
            req.Hobbies
        );

        Result<CandidateDto> result = await Resolve<ISender>().Send(command, ct);
    }
}
