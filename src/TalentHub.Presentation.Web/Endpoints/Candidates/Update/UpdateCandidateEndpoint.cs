using System;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Update;

public sealed class UpdateCandidateEndpoint : Ep.Req<UpdateCandidateRequest>.NoRes
{
    public override void Configure()
    {
        Put("{candidateId:guid}");
        Validator<UpdateCandidateRequestValidator>();
        Group<CandidatesEndpointsGroup>();
    }

    public override async Task HandleAsync(UpdateCandidateRequest req, CancellationToken ct)
    {
        Guid candidateId = Route<Guid>("candidateId");

        UpdateCandidateCommand command = new(
            candidateId,
            req.Name,
            req.AutoMatchEnabled,
            req.Phone, 
            req.AddressStreet,
            req.AddressNumber,
            req.AddressNeighborhood,
            req.AddressCity,
            req.AddressState,
            req.AddressCountry,
            req.AddressZipCode,
            req.DesiredWorkplaceTypes,
            req.DesiredJobTypes,
            req.ExpectedRemuneration,
            req.InstagramUrl,
            req.LinkedInUrl,
            req.GitHubUrl,
            req.Summary,
            req.Hobbies
        );

        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(command, ct)
            )
        );
    }
}
