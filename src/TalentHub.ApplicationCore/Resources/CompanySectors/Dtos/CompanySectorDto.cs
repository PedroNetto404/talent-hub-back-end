namespace TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;

public sealed record CompanySectorDto(Guid Id, string Name)
{
    public static CompanySectorDto FromEntity(CompanySector entity) =>
        new(entity.Id, entity.Name);
}
