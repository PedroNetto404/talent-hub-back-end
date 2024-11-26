using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateAcademicExperience;

public sealed class UpdateAcademicExperienceCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<UpdateExperienceCommand>
{
    public async Task<Result> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return NotFoundError.Value;

        var start = DatePeriod.Create(request.StartYear, request.StartMonth);
        if (start.IsFail) return start.Error;

        var end = request is { EndMonth: not null, EndYear: not null }
        ? DatePeriod.Create(request.EndYear!.Value, request.EndMonth!.Value)
        : Result.Ok<DatePeriod>(null!);
        if (end.IsFail) return end.Error;

        var result = request.Type switch
        {
            "academic" =>
            Enum.TryParse<ProgressStatus>(request.Status, true, out var status)
                ? candidate.UpdateExperience(
                    request.ExperienceId,
                    start.Value,
                    end.Value,
                    request.IsCurrent,
                    request.Activities,
                    status)
                : Result.Fail(new Error("candidate_experience", "Invalid status")),
            "professional" => candidate.UpdateExperience(
                request.ExperienceId,
                start.Value,
                end.Value,
                request.IsCurrent,
                request.Activities,
                request.Description!),
            _ => Result.Fail(new Error("candidate_experience", "Invalid experience type"))
        };
        if (result.IsFail) return result.Error;

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}