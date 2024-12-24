using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Delete;

public sealed class DeleteCandidateCertificateCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<DeleteCandidateCertificateCommand>
{
    public async Task<Result> Handle(
        DeleteCandidateCertificateCommand request, 
        CancellationToken cancellationToken
    )
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if(candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if(candidate.RemoveCertificate(request.CertificateId) is { IsFail: true, Error: var removeError})
        {
            return removeError;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
