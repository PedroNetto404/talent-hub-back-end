using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Skills;
using TalentHub.ApplicationCore.Resources.Skills.Specs;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Update;

public sealed class UpdateCourseCommandHandler(
    IRepository<Course> courseRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<UpdateCourseCommand>
{
    public async Task<Result> Handle(
        UpdateCourseCommand request, 
        CancellationToken cancellationToken
    )
    {
        Course? course = await courseRepository.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return Error.NotFound("course");
        }

        if (course.ChangeName(request.Name) is { IsFail: true, Error: var nameError })
        {
            return nameError;
        }

        course.ClearTags();

        foreach (string tag in request.Tags)
        {
            if (course.AddTag(tag) is { IsFail: true, Error: var tagError })
            {
                return tagError;
            }
        }

        List<Skill> skills = await skillRepository.ListAsync(
            new GetSkillsSpec(request.RelatedSkills),
            cancellationToken
        );

        if (skills.Count != request.RelatedSkills.Count())
        {
            return Error.BadRequest("some skills not found");
        }

        foreach (Skill skill in skills)
        {
            if (course.AddRelatedSkill(skill.Id) is { IsFail: true, Error: var skillError })
            {
                return skillError;
            }
        }

        await courseRepository.UpdateAsync(course, cancellationToken);
        return Result.Ok();
    }
}
