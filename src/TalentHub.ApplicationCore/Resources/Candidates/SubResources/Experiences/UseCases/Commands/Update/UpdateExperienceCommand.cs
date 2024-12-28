using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.Update;

public sealed record UpdateExperienceCommand(
    Guid CandidateId,
    Guid ExperienceId,
    string Type,
    DatePeriod Start,
    DatePeriod? End,
    bool IsCurrent,
    IEnumerable<string> Activities,
    IEnumerable<string>? AcademicEntities,
    int? CurrentSemester,
    string? Status,
    string? Description
) : ICommand<CandidateDto>;
