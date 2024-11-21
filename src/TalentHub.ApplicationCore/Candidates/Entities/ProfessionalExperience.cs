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
    public string Position { get; private set; }
    public string Description { get; private set; }
    public string Company { get; private set; }
    public ProfessionalLevel Level { get; private set; }

    public Result ChangeDescription(string newDescription)
    {
        newDescription = newDescription.Trim();

        if(string.IsNullOrWhiteSpace(newDescription)) 
            return new Error("professional_experience", "Invalid description");

        Description = newDescription;

        return Result.Ok();
    }
}