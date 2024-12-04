using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.CreateCandidateSkill;

public sealed record CreateCandidateSkillCommand(
    Guid CandidateId,
    Guid SkillId,
    string Proficiency
) : ICommand<CandidateDto>;