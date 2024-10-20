using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public sealed class ProfessionalExperience : Experience 
{
    public string Position { get; private set; }
    public string Description { get; private set; }
    public string CompanyName { get; private set; }
    public ProfessionalLevel Level { get; private set; }

    public Result ChangeDescription(string newDescription)
    {
        newDescription = newDescription.Trim();

        if(string.IsNullOrWhiteSpace(newDescription)) 
            return Error.Displayable("professional_experience", "Invalid description");

        Description = newDescription;

        return Result.Ok();
    }
}