using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.Update;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.UpdateAcademicExperience;

public sealed class UpdateExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<UpdateExperienceCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateExperienceCommand request, 
        CancellationToken cancellationToken
    )
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result result = request.Type switch
        {
            "academic" =>
                Enum.TryParse(request.Status, true, out ProgressStatus progressStatus)
                    ? candidate.UpdateExperience(
                        request.ExperienceId,
                        request.Start,
                        request.End,
                        request.CurrentSemester!.Value,
                        request.IsCurrent,
                        request.Activities,
                        request.AcademicEntities ?? [],
                        progressStatus)
                    : Error.InvalidInput($"{request.Status} is not valid progress status"),
            "professional" => candidate.UpdateExperience(
                request.ExperienceId,
                request.Start,
                request.End,
                request.IsCurrent,
                request.Activities,
                request.Description!),
            _ => Error.InvalidInput($"{request.Type} must be either academic or professional")
        };
        if (result.IsFail)
        {
            return result.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
