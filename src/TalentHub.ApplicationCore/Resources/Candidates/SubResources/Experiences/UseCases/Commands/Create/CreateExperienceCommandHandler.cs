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
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(
            new GetCandidateByIdSpec(request.CandidateId),
             cancellationToken
        );
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        Result<Experience> experience = request.Type switch
        {
            "academic" => await CreateAcademicExperienceAsync(request, cancellationToken),
            "professional" => CreateProfessionalExperience(request),
            _ => Error.InvalidInput("invalid experience type")
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

    private static Result<Experience> CreateProfessionalExperience(CreateExperienceCommand request)
    {
        if (!Enum.TryParse(request.ProfessionalLevel, true, out ProfessionalLevel level))
        {
            return Error.InvalidInput($"{request.ProfessionalLevel} is not valid professional level");
        }

        Result<ProfessionalExperience> experience = ProfessionalExperience.Create(
            request.Start,
            request.End,
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

    private async Task<Result<Experience>> CreateAcademicExperienceAsync(
        CreateExperienceCommand request,
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

        if (!Enum.TryParse(request.EducationalLevel.Pascalize(), true, out EducationLevel level))
        {
            return Error.InvalidInput($"{request.EducationalLevel} is not valid educational level");
        }

        if (!Enum.TryParse(request.ProgressStatus.Pascalize(), true, out ProgressStatus status))
        {
            return Error.InvalidInput($"{request.ProgressStatus} is not valid progress status");
        }

        Result<AcademicExperience> experienceResult = AcademicExperience.Create(
            request.Start,
            request.End,
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
                return Error.InvalidInput($"{academicEntity} is not valid academic entity");
            }

            if (experience.AddAcademicEntity(entity) is { IsFail: true, Error: var error })
            {
                return error;
            }
        }

        return experience;
    }
}
