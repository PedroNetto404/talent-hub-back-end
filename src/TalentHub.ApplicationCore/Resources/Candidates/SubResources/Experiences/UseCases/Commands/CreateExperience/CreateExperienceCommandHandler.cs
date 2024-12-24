using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands;
using TalentHub.ApplicationCore.Resources.Courses;
using TalentHub.ApplicationCore.Resources.Universities;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.CreateExperience;

public sealed class CreateExperienceCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<University> educationalInstituteRepository,
    IRepository<Course> courseRepository
) :
    ICommandHandler<CreateExperienceCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateExperienceCommand request,
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result<DatePeriod> startResult = DatePeriod.Create(request.StartYear, request.StartMonth);
        if (startResult is { IsFail: true, Error: var startResultError})
        {
            return startResultError;
        }

        Result<DatePeriod> end = request is { EndMonth: not null, EndYear: not null }
            ? DatePeriod.Create(request.EndYear!.Value, request.EndMonth!.Value)
            : Result.Ok<DatePeriod>(null!);
        if (end.IsFail)
        {
            return end.Error;
        }

        Result<Experience> experience = request.Type switch
        {
            "academic" => await CreateAcademicExperience(request, startResult.Value, end.Value, cancellationToken),
            "professional" => CreateProfessionalExperience(request, startResult.Value, end.Value),
            _ => Error.BadRequest("invalid experience type")
        };
        if (experience.IsFail)
        {
            return experience.Error;
        }

        Result result = candidate.AddExperience(experience.Value);
        if (result.IsFail)
        {
            return result.Error;
        }

        foreach (string activity in request.Activities)
        {
            Result activityResult = experience.Value.AddActivity(activity);
            if (activityResult.IsFail)
            {
                return activityResult.Error;
            }
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }

    private static Result<Experience> CreateProfessionalExperience(
        CreateExperienceCommand request,
        DatePeriod start,
        DatePeriod? end)
    {
        if (!Enum.TryParse(request.ProfessionalLevel, true, out ProfessionalLevel level))
        {
            return Error.BadRequest($"{request.ProfessionalLevel} is not valid professional level");
        }

        Result<ProfessionalExperience> experience = ProfessionalExperience.Create(
            start,
            end,
            request.IsCurrent,
            request.Position!,
            request.Company!,
            request.Description!,
            level
        );
        return experience.IsFail
            ? experience.Error
            : Result.Ok<Experience>(experience.Value);
    }

    private async Task<Result<Experience>> CreateAcademicExperience(
        CreateExperienceCommand request,
        DatePeriod start,
        DatePeriod? end,
        CancellationToken cancellationToken)
    {
        Course? course = await courseRepository.GetByIdAsync(request.CourseId!.Value, cancellationToken);
        if (course is null)
        {
            return Error.NotFound("course");
        }

        University? university =
            await educationalInstituteRepository.GetByIdAsync(
                request.UniversityId!.Value, 
                cancellationToken);
        if (university is null)
        {
            return Error.NotFound("univesity");
        }

        if (!Enum.TryParse(request.Level.Pascalize(), true, out EducationLevel level))
        {
            return Error.BadRequest($"{request.Level} is not valid educational level");
        }

        if (!Enum.TryParse(request.Status.Pascalize(), true, out ProgressStatus status))
        {
            return Error.BadRequest($"{request.Status} is not valid progress status");
        }
        
        Result<AcademicExperience> experienceResult = AcademicExperience.Create(
            start,
            end,
            request.CurrentSemester!.Value,
            request.IsCurrent,
            level,
            status,
            course.Id,
            university.Id
        );
        if (experienceResult.IsFail)
        {
            return experienceResult.Error;
        }
        
        AcademicExperience experience = experienceResult.Value;

        foreach (string academicEntity in request.AcademicEntities)
        {
            if (!Enum.TryParse(academicEntity, true, out AcademicEntity entity))
            {
                return Error.BadRequest($"{academicEntity} is not valid academic entity");
            }

            if (experience.AddAcademicEntity(entity) is { IsFail: true, Error: var error })
            {
                return error;
            }
        }

        return experience;
    }
}
