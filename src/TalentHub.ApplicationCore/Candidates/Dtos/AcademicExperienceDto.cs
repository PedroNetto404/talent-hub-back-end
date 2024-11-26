using Humanizer;
using TalentHub.ApplicationCore.Candidates.Entities;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public record ExperienceDto(
    Guid Id,
    int StartMonth,
    int StartYear,
    int? EndMonth,
    int? EndYear,
    bool IsCurrent,
    string Type,
    IEnumerable<string> Activities,
    string? EducationLevel,
    string? EducationProgressStatus,
    Guid? CourseId,
    Guid? InstitutionId,
    string? Position,
    string? Description,
    string? Company,
    string? ProfessionalLevel
)
{
    public static ExperienceDto FromEntity(
        Experience experience
    ) => experience switch
    {
        AcademicExperience academicExperience => new ExperienceDto(
            experience.Id,
            academicExperience.Start.Month,
            academicExperience.Start.Year,
            academicExperience.End?.Month,
            academicExperience.End?.Year,
            academicExperience.IsCurrent,
            "academic",
            academicExperience.Activities,
            academicExperience.Level.ToString().Underscore(),
            academicExperience.Status.ToString().Underscore(),
            academicExperience.CourseId,
            academicExperience.InstitutionId,
            null,
            null,
            null,
            null
        ),
        ProfessionalExperience professionalExperience => new ExperienceDto(
            experience.Id,
            professionalExperience.Start.Month,
            professionalExperience.Start.Year,
            professionalExperience.End?.Month,
            professionalExperience.End?.Year,
            professionalExperience.IsCurrent,
            "professional",
            professionalExperience.Activities,
            null,
            null,
            null,
            null,
            professionalExperience.Position,
            professionalExperience.Description,
            professionalExperience.Company,
            professionalExperience.Level.ToString().Underscore()
        ),
        _ => throw new InvalidOperationException("Invalid experience type")
    };
}