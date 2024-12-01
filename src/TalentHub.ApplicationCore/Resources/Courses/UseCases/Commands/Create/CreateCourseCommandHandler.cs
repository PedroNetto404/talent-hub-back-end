using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Skills;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create;

public sealed class CreateCourseCommandHandler(
    IRepository<Course> courseRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<CreateCourseCommand, CourseDto>
{
    public async Task<Result<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        Course? existingCourse = await courseRepository.FirstOrDefaultAsync(
            (query) => query.Where(c => c.Name == request.Name),
            cancellationToken);
        if (existingCourse is not null)
        {
            return Error.BadRequest("course with this name already exists");
        }

        List<Skill> skills = await skillRepository.ListAsync(
            (query) => query.Where(s => request.RelatedSkills.Contains(s.Id)), 
            cancellationToken);
        if (skills.Count != request.RelatedSkills.Count())
        {
            return Error.BadRequest("invalid skills");
        }

        Result<Course> courseResult = Course.Create(request.Name);
        if (courseResult.IsFail) 
        {
            return courseResult.Error;
        }

        foreach (string tag in request.Tags)
        {
            if(courseResult.Value.AddTag(tag) is { IsFail: true, Error: var err})
            {
                return err;
            }
        }

        foreach (Skill skill in skills)
        {
            if(courseResult.Value.AddRelatedSkill(skill.Id) is { IsFail: true, Error: var skillError})
            {
                return skillError;
            }
        }

        await courseRepository.AddAsync(courseResult.Value, cancellationToken);
        return CourseDto.FromEntity(courseResult.Value);
    }
}
