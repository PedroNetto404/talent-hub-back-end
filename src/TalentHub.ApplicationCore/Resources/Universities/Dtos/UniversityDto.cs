namespace TalentHub.ApplicationCore.Resources.Universities.Dtos;

public record UniversityDto(
    Guid Id,
    string Name,
    string? SiteUrl)
{
    public static UniversityDto FromEntity(University university) =>
        new(
            university.Id,
            university.Name,
            university.SiteUrl
        );
}
