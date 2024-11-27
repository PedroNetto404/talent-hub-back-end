using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateAcademicExperience;

public sealed record CreateExperienceCommand(
    Guid CandidateId,
    string Type,
    int StartMonth,
    int StartYear,
    int? EndMonth,
    int? EndYear,
    bool IsCurrent,
    IEnumerable<string> Activities,
    string? Level,
    string? Status,
    int? CurrentSemester,
    IEnumerable<string> AcademicEntities,
    Guid? CourseId,
    Guid? InstitutionId,
    string? Position,
    string? Description,
    string? Company,
    string? ProfessionalLevel
) : ICommand<CandidateDto>;
