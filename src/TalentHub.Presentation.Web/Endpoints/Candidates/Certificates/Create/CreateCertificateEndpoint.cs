using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.Create;

public sealed class CreateCertificateEndpoint : Ep.Req<CreateCertificateRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");

        Version(1);
        Group<CandidateCertificatesEndpointsGroup>();

        Description(builder => builder.Accepts<CreateCertificateRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Create a new certificate for a candidate.")
            .WithDisplayName("Create Candidate Certificate")
        );
    }

    public override async Task HandleAsync(CreateCertificateRequest req, CancellationToken ct)
    {
        Guid candidateId = Route<Guid>("candidateId");
        
        CreateCandidateCertificateCommand command = new(
            candidateId,
            req.Name,
            req.Issuer,
            req.Workload,
            req.RelatedSkills);

        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(command, ct)
            )
        );
    }
}
