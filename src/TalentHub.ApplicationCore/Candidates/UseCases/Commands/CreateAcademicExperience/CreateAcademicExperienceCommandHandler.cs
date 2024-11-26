using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.EducationalInstitutes;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateAcademicExperience;

public sealed class CreateAcademicExperienceCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<EducationalInstitute> educationalInstituteRepository,
    IRepository<Course> courseRepository
) :
    ICommandHandler<CreateExperienceCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateExperienceCommand request,
        CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return NotFoundError.Value;

        var start = DatePeriod.Create(request.StartYear, request.StartMonth);
        if (start.IsFail) return start.Error;

        var end = request is { EndMonth: not null, EndYear: not null }
        ? DatePeriod.Create(request.EndYear!.Value, request.EndMonth!.Value)
        : Result.Ok<DatePeriod>(null!);
        if (end.IsFail) return end.Error;

        var experience = request.Type switch
        {
            "academic" => await CreateAcademicExperience(request, start.Value, end.Value, cancellationToken),
            "professional" => CreateProfessionalExperience(request, start.Value, end.Value),
            _ => Result.Fail<Experience>(new Error("candidate_experience", "Invalid experience type"))
        };
        if (experience.IsFail) return experience.Error;

        var result = candidate.AddExperience(experience.Value);
        if (result.IsFail) return result.Error;

        foreach (var activity in request.Activities)
        {
            var activityResult = experience.Value.AddActivity(activity);
            if (activityResult.IsFail) return activityResult.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }

    private static Result<Experience> CreateProfessionalExperience(
        CreateExperienceCommand request,
        DatePeriod start,
        DatePeriod? end)
    {
        if (!Enum.TryParse<ProfessionalLevel>(request.ProfessionalLevel, true, out var level))
            return Result.Fail<Experience>(new Error("candidate_experience", "Invalid professional level"));

        var experience = ProfessionalExperience.Create(
            start,
            end,
            request.IsCurrent,
            request.Position!,
            request.Company!,
            request.Description!,
            level
        );
        if (experience.IsFail) return experience.Error;

        return Result.Ok<Experience>(experience.Value);
    }

    private async Task<Result<Experience>> CreateAcademicExperience(
        CreateExperienceCommand request,
        DatePeriod start,
        DatePeriod? end,
        CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.CourseId!.Value, cancellationToken);
        if (course is null) return NotFoundError.Value;

        var institution = await educationalInstituteRepository.GetByIdAsync(request.InstitutionId!.Value, cancellationToken);
        if (institution is null) return NotFoundError.Value;

        if (!Enum.TryParse<EducationLevel>(request.Level, true, out var level))
            return Result.Fail<Experience>(new Error("candidate_experience", "Invalid professional level"));

        if (!Enum.TryParse<ProgressStatus>(request.Status, true, out var status))
            return Result.Fail<Experience>(new Error("candidate_experience", "Invalid status"));

        var experience = AcademicExperience.Create(
            start,
            end,
            request.IsCurrent,
            level,
            status,
            course.Id,
            institution.Id
        );
        if (experience.IsFail) return experience.Error;

        return Result.Ok<Experience>(experience.Value);
    }
}