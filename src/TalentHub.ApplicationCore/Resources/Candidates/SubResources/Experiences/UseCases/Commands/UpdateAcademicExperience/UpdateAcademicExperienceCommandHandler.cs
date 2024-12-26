using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.UpdateAcademicExperience;

public sealed class UpdateAcademicExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<UpdateExperienceCommand>
{
    public async Task<Result> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result<DatePeriod> start = DatePeriod.Create(request.StartYear, request.StartMonth);
        if (start.IsFail)
        {
            return start.Error;
        }

        Result<DatePeriod> endResult = request is { EndMonth: not null, EndYear: not null }
            ? DatePeriod.Create(request.EndYear!.Value, request.EndMonth!.Value)
            : Result.Ok<DatePeriod>(null!);
        if (endResult.IsFail)
        {
            return endResult.Error;
        }

        Result result = request.Type switch
        {
            "academic" =>
                Enum.TryParse(request.Status, true, out ProgressStatus progressStatus)
                    ? candidate.UpdateExperience(
                        request.ExperienceId,
                        start.Value,
                        endResult.Value,
                        request.CurrentSemester!.Value,
                        request.IsCurrent,
                        request.Activities,
                        request.AcademicEntities,
                        progressStatus)
                    : Error.InvalidInput($"{request.Status} is not valid progress status"),
            "professional" => candidate.UpdateExperience(
                request.ExperienceId,
                start.Value,
                endResult.Value,
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
        return Result.Ok();
    }
}
