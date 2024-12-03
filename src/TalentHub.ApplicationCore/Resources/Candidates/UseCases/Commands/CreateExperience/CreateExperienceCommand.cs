using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateAcademicExperience;

public sealed record CreateExperienceCommand(
    Guid CandidateId,
    string Type,
    int StartMonth,
    int StartYear,
    int? EndMonth,
    int? EndYear,
    int? ExpectedGraduationStartMonth,
    int? ExpectedGraduationStartYear,
    bool IsCurrent,
    IEnumerable<string> Activities,
    string? Level,
    string? Status,
    int? CurrentSemester,
    IEnumerable<string> AcademicEntities,
    Guid? CourseId,
    Guid? UniversityId,
    string? Position,
    string? Description,
    string? Company,
    string? ProfessionalLevel
) : ICommand<CandidateDto>;
