using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Skills.Specs;

public sealed class GetSkillsByIdsSpec : Specification<Skill>
{
    public GetSkillsByIdsSpec(params Guid[] ids) => 
        Query.Where(p => ids.Contains(p.Id));
}
