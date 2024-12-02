using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteCandidateCertificate;

public sealed class DeleteCandidateCertificateCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<DeleteCandidateCertificateCommand>
{
    public async Task<Result> Handle(
        DeleteCandidateCertificateCommand request, 
        CancellationToken cancellationToken
    )
    {
        (Guid candidateId, Guid certificateId) = request;

        Candidate? candidate = await candidateRepository.GetByIdAsync(
            candidateId,
            cancellationToken
        );
        if(candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if(candidate.RemoveCertificate(certificateId) is { IsFail: true, Error: var removeError})
        {
            return removeError;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
