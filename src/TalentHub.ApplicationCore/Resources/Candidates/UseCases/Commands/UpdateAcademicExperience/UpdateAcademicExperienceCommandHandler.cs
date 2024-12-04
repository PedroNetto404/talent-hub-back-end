using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateAcademicExperience;

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

        Result<Candidate> candidate =
            await candidateRepository
                .GetByIdAsync(candidateId, cancellationToken)
                .FailIfNullAsync(() => Error.NotFound("candidate"));

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
                Enum.TryParse(request.Status, true, out ProgressStatus status)
                    ? candidate.UpdateExperience(
                        request.ExperienceId,
                        start.Value,
                        endResult.Value,
                        request.CurrentSemester!.Value,
                        request.IsCurrent,
                        request.Activities,
                        request.AcademicEntities,
                        status)
                    : Error.BadRequest($"{status} is not valid progress status"),
            "professional" => candidate.UpdateExperience(
                request.ExperienceId,
                start.Value,
                endResult.Value,
                request.IsCurrent,
                request.Activities,
                request.Description!),
            _ => Result.Fail(new Error("candidate_experience", "Invalid experience type"))
        };
        if (result.IsFail)
        {
            return result.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
