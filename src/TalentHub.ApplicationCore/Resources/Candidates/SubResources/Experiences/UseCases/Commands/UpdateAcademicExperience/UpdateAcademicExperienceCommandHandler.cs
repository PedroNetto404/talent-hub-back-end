using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.UpdateAcademicExperience;

public sealed class UpdateAcademicExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<UpdateExperienceCommand>
{
    public async Task<Result> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        (
            Guid candidateId,
            Guid experienceId,
            string type,
            int startMonth,
            int startYear,
            int? endMonth,
            int? endYear,
            bool isCurrent,
            IEnumerable<string> activities,
            IEnumerable<string> academicEntities,
            int? currentSemester,
            string? status,
            string? description
        ) = request;

        Candidate? candidate = await candidateRepository.GetByIdAsync(candidateId, cancellationToken);
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

        Result result = type switch
        {
            "academic" =>
                Enum.TryParse(request.Status, true, out ProgressStatus progressStatus)
                    ? candidate.UpdateExperience(
                        experienceId,
                        start.Value,
                        endResult.Value,
                        currentSemester!.Value,
                        isCurrent,
                        activities,
                        academicEntities,
                        progressStatus)
                    : Error.BadRequest($"{status} is not valid progress status"),
            "professional" => candidate.UpdateExperience(
                experienceId,
                start.Value,
                endResult.Value,
                isCurrent,
                activities,
                description!),
            _ => Error.BadRequest($"{type} must be either academic or professional")
        };
        if (result.IsFail)
        {
            return result.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
