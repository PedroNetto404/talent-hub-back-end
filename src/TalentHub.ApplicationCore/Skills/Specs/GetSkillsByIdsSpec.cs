using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Skills.Specs;

public sealed class GetSkillsByIdsSpec : Specification<Skill>
{
    public GetSkillsByIdsSpec(params Guid[] ids) =>
        Query.Where(p => ids.Contains(p.Id));
}

public sealed class GetSkillByNameSpec : Specification<Skill>
{
    public GetSkillByNameSpec(string name) =>
        Query.Where(p => p.Name == name).AsNoTracking();
}