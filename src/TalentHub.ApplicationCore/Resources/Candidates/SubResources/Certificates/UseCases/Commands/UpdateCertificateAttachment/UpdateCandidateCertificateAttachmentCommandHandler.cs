using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.UpdateCertificateAttachment;

public sealed class UpdateCandidateCertificateAttachmentCommandHandler(
    IRepository<Candidate> candidateRepository,
    IFileStorage fileStorage) : 
    ICommandHandler<UpdateCandidateCertificateAttachmentCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateCertificateAttachmentCommand request, 
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Certificate? certificate = candidate.Certificates.FirstOrDefault(p => p.Id == request.CertificateId);
        if (certificate is null)
        {
            return Error.BadRequest($"certificate with id {request.CertificateId} not found");
        }

        if (certificate.AttachmentUrl is not null)
        {
            await fileStorage.DeleteAsync(
                FileBucketNames.CandidateCertificateAttchments,
                certificate.AttachmentFileName,
                cancellationToken);
        }
        
        string url = await fileStorage.SaveAsync(
            FileBucketNames.CandidateCertificateAttchments,
            request.AttachmentFile,
            $"{certificate.AttachmentFileName}.{request.ContentType.Split("/").Last()}",
            request.ContentType,
            cancellationToken
        );
        if (!url.IsValidUrl())
        {
            return Error.Unexpected("failed to save attachment");
        }
        
        if (certificate.ChangeAttachmentUrl(url) is { IsFail: true, Error: var changeError})
        {
            return changeError;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
