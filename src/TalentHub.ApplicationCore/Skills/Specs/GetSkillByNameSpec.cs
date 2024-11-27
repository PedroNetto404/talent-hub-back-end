using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Skills.Specs;

public sealed class GetSkillByNameSpec : Specification<Skill>
{
    public GetSkillByNameSpec(string name) =>
        Query.Where(p => p.Name == name).AsNoTracking();
}