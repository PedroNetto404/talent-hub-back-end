using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Create;

public sealed record CreateCandidateSkillCommand(
    Guid CandidateId,
    Guid SkillId,
    string Proficiency
) : ICommand<CandidateDto>;