using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills;
using TalentHub.ApplicationCore.Skills.Specs;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands.Update;

public sealed class UpdateCourseCommandHandler(
    IRepository<Course> courseRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<UpdateCourseCommand>
{
    public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.Id, cancellationToken);
        if(course is null) return NotFoundError.Value;

        if(course.ChangeName(request.Name) is { IsFail: true, Error: var nameError })
            return nameError;

        course.ClearTags(); 

        foreach(var tag in request.Tags)
            if(course.AddTag(tag) is {IsFail: true, Error: var tagError})
                return tagError;

        var skills = await skillRepository.ListAsync(
            new GetSkillsByIdsSpec([.. request.RelatedSkills]),
            cancellationToken
        );

        if(skills.Count != request.RelatedSkills.Count())
            return new Error("course", "invalid relared skills");

        foreach(var skill in skills)
            if(course.AddRelatedSkill(skill.Id) is { IsFail: true, Error: var skillError})
                return skillError;

        await courseRepository.UpdateAsync(course, cancellationToken);
        return Result.Ok();
    }
}