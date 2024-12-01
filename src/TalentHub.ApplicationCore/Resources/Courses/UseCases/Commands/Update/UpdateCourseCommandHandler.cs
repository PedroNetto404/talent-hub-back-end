using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Skills;

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
        (
            Guid id,
            string name,
            IEnumerable<string> tags,
            IEnumerable<Guid> relatedSkills
        ) = request;

        Course? course = await courseRepository.GetByIdAsync(id, cancellationToken);
        if (course is null)
        {
            return Error.NotFound("course");
        }

        if (course.ChangeName(name) is { IsFail: true, Error: var nameError })
        {
            return nameError;
        }

        course.ClearTags();

        foreach (string tag in tags)
        {
            if (course.AddTag(tag) is { IsFail: true, Error: var tagError })
            {
                return tagError;
            }
        }

        List<Skill> skills = await skillRepository.ListAsync(
            (query) => query.Where(s => request.RelatedSkills.Contains(s.Id)),
            cancellationToken
        );

        if (skills.Count != relatedSkills.Count())
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
