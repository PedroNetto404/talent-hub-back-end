using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.Create;

public sealed class CreateCertificateEndpoint : Ep.Req<CreateCertificateRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");
        Description(builder => builder.Accepts<CreateCertificateRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
        );
        Version(1);
        Group<CandidateCertificatesEndpointsSubGroup>();
        Validator<CreateCertificateRequestValidator>();
    }

    public override Task HandleAsync(CreateCertificateRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new CreateCandidateCertificateCommand(
                req.CandidateId,
                req.Name,
                req.Issuer,
                req.Workload,
                req.RelatedSkills
            ), 
            ct
        );
}
