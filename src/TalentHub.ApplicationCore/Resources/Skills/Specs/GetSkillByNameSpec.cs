using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Skills.Specs;

public sealed class GetSkillByNameSpec : Specification<Skill>
{
    public GetSkillByNameSpec(string name)
    {
        Query.Where(s => s.Name == name).AsNoTracking();
    }
}
