using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.
    UpdateCertificateAttachment;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.UpdateAttachment;

public class UpdateCandidateCertificateAttachmentEndpoint :
    Ep.Req<UpdateCandidateCertificateAttachmentRequest>
    .Res<CandidateDto>
{
    public override void Configure()
    {
        Patch("{certificateId:guid}/attachment");
        Description(builder => builder.Accepts<UpdateCandidateCertificateAttachmentRequest>()
            .Produces<CandidateDto>()
            .Produces(StatusCodes.Status400BadRequest)
        );
        Validator<UpdateCandidateCertificateAttachmentRequestValidator>();
        Group<CandidateCertificatesEndpointsSubGroup>();
        Version(1);
    }

    public override async Task HandleAsync(UpdateCandidateCertificateAttachmentRequest req, CancellationToken ct)
    {
        using MemoryStream ms = new();
        await req.File.CopyToAsync(ms, ct);

        UpdateCandidateCertificateAttachmentCommand command = new(
            req.CandidateId,
            req.CertificateId,
            ms,
            req.File.ContentType
        );

        await this.HandleUseCaseAsync(command, ct);
    }
}
