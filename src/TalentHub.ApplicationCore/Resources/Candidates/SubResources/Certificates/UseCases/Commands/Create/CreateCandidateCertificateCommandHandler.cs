using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Skills;
using TalentHub.ApplicationCore.Resources.Skills.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands.Create;

public sealed class CreateCandidateCertificateCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<CreateCandidateCertificateCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateCandidateCertificateCommand request,
        CancellationToken cancellationToken)
    {
        List<Skill> skills = await skillRepository.ListAsync(
            new GetSkillsSpec(
                request.RelatedSkills,
                int.MaxValue,
                0
            ),
            cancellationToken
        );
        if (skills.Count != request.RelatedSkills.Count())
        {
            return Error.BadRequest("invalid skills");
        }

        Candidate? candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result<Certificate> certificateResult = Certificate.Create(
            request.Name,
            request.Issuer,
            request.Workload);
        if (certificateResult.IsFail)
        {
            return certificateResult.Error;
        }

        foreach (Guid relatedSkill in request.RelatedSkills)
        {
            if (certificateResult.Value.AddRelatedSkill(relatedSkill) is
                {
                    IsFail: true, Error: var error
                })
            {
                return error;
            }
        }

        Result addResult = candidate.AddCertificate(certificateResult.Value);
        if (addResult.IsFail)
        {
            return addResult.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
