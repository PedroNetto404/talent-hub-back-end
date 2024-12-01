using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TalentHub.ApplicationCore.Resources.Candidates.Entities;

public sealed class ProfessionalExperience : Experience
{
    protected ProfessionalExperience() { }

    private ProfessionalExperience(
        DatePeriod start,
        DatePeriod? end,
        bool isCurrent,
        string position,
        string company,
        string description,
        ProfessionalLevel level) : base(start, end, isCurrent)
    {
        Position = position;
        Description = description;
        Company = company;
        Level = level;
    }

    public static Result<ProfessionalExperience> Create(
        DatePeriod start,
        DatePeriod? end,
        bool isCurrent,
        string position,
        string company,
        string description,
        ProfessionalLevel level)
    {
        if (end != null && start > end)
        {
            return Error.BadRequest("start date must be less than end date");
        }

        if (string.IsNullOrWhiteSpace(position))
        {
            return Error.BadRequest("position must be provided.");
        }

        if (string.IsNullOrWhiteSpace(company))
        {
            return Error.BadRequest("company must be provided.");
        }

        return !string.IsNullOrWhiteSpace(description)
            ? Result.Ok(new ProfessionalExperience(
                start,
                end,
                isCurrent,
                position,
                company,
                description,
                level
            ))
            : Error.BadRequest("Description must be provided.");
    }

    public string Position { get; private set; }
    public string Description { get; private set; }
    public string Company { get; private set; }
    public ProfessionalLevel Level { get; private set; }

    public Result ChangeDescription(string description)
    {
        description = description.Trim();

        if (string.IsNullOrWhiteSpace(description))
        {
            return Error.BadRequest("description is required");
        }

        Description = description;

        return Result.Ok();
    }
}
