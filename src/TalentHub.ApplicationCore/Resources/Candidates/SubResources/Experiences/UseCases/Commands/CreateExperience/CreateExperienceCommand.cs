using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.CreateExperience;

public sealed record CreateExperienceCommand(
    Guid CandidateId,
    string Type,
    DatePeriod Start,
    DatePeriod? End,
    bool IsCurrent,
    IEnumerable<string> Activities,
    DatePeriod? ExpectedGraduation,
    string? EducationalLevel,
    string? ProgressStatus,
    int? CurrentSemester,
    IEnumerable<string> AcademicEntities,
    Guid? CourseId,
    Guid? UniversityId,
    string? Position,
    string? Description,
    string? Company,
    string? ProfessionalLevel
) : ICommand<CandidateDto>;
