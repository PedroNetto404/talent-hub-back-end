using TalentHub.ApplicationCore.Candidates.Entities;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record ExperienceDto(
    Guid Id,
    int StartMonth,
    int StartYear,
    int? EndMonth,
    int? EndYear,
    bool IsCurrent,
    IEnumerable<string> Activities
)
{
    public static ExperienceDto FromEntity(Experience experience) =>
        new(
            experience.Id,
            experience.Start.Month,
            experience.Start.Year,
            experience.End?.Month,
            experience.End?.Year,
            experience.IsCurrent,
            experience.Activities
        );
}