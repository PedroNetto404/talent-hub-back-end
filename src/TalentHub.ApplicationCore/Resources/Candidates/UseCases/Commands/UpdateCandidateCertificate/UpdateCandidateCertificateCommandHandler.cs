using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills;
using TalentHub.ApplicationCore.Resources.Skills.Specs;

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
        if(request.RelatedSkills.Any())
        {
            IEnumerable<Skill> skills = await skillRepository.ListAsync(
                new GetSkillsSpec(request.RelatedSkills, int.MaxValue, 0),
                cancellationToken
            );

            if(skills.Count() != request.RelatedSkills.Count())
            {
                return Error.NotFound("skill");
            }
        }

        Candidate? candidate = await candidateRepository.GetByIdAsync(
            request.CandidateId,
            cancellationToken
        );
        if(candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result updateResult = candidate.UpdateCertificate(
            request.CertificateId,
            request.Name,
            request.Issuer,
            request.Workload,
            request.Url,
            request.RelatedSkills
        );
        if(updateResult.IsFail)
        {
            return updateResult.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
