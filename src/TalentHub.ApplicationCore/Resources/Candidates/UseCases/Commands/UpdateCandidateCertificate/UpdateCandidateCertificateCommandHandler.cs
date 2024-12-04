using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Skills;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateCertificate;

public sealed class UpdateCandidateCertificateCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) :
    ICommandHandler<UpdateCandidateCertificateCommand>
{
    public async Task<Result> Handle(
        UpdateCandidateCertificateCommand request, 
        CancellationToken cancellationToken
    )
    {
        (
            Guid candidateId,
            Guid certificateId,
            string name,
            string issuer,
            double workload,
            string url,
            IEnumerable<Guid> relatedSkills
        ) = request;

        if(relatedSkills.Any())
        {
            IEnumerable<Skill> skills = await skillRepository.GetManyByIdsAsync(
                relatedSkills,
                cancellationToken
            );
            if(skills.Count() != relatedSkills.Count())
            {
                return Error.NotFound("skill");
            }
        }

        Candidate? candidate = await candidateRepository.GetByIdAsync(
            candidateId,
            cancellationToken
        );
        if(candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result updateResult = candidate.UpdateCertificate(
            certificateId,
            name,
            issuer,
            workload,
            url,
            relatedSkills
        );
        if(updateResult.IsFail)
        {
            return updateResult.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}