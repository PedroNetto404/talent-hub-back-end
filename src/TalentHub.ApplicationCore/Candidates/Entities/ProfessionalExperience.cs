using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.ValueObjects;
#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TalentHub.ApplicationCore.Candidates.Entities;

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
            return new Error("experience", "Start date must be less than end date.");

        if (string.IsNullOrWhiteSpace(position))
            return new Error("experience", "Position must be provided.");

        if (string.IsNullOrWhiteSpace(company))
            return new Error("experience", "Company must be provided.");

        if (string.IsNullOrWhiteSpace(description))
            return new Error("experience", "Description must be provided.");

        return Result.Ok(new ProfessionalExperience(start, end, isCurrent, position, company, description, level));
    }

    public string Position { get; private set; }
    public string Description { get; private set; }
    public string Company { get; private set; }
    public ProfessionalLevel Level { get; private set; }

    public Result ChangeDescription(string newDescription)
    {
        newDescription = newDescription.Trim();

        if (string.IsNullOrWhiteSpace(newDescription))
            return new Error("professional_experience", "Invalid description");

        Description = newDescription;

        return Result.Ok();
    }
}