using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateAcademicExperience;

public sealed record UpdateExperienceCommand(
    Guid CandidateId,
    Guid ExperienceId,
    string Type,
    int StartMonth,
    int StartYear,
    int? EndMonth,
    int? EndYear,
    bool IsCurrent,
    IEnumerable<string> Activities,
    IEnumerable<string> AcademicEntities,
    int? CurrentSemester,
    string? Status,
    string? Description
) : ICommand;