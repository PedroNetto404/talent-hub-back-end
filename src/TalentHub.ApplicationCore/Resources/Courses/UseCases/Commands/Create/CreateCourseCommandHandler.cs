using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Courses.Dtos;
using TalentHub.ApplicationCore.Courses.Specs;
using TalentHub.ApplicationCore.Skills;
using TalentHub.ApplicationCore.Skills.Specs;

namespace TalentHub.ApplicationCore.Courses.UseCases.Commands.Create;

public sealed class CreateCourseCommandHandler(
    IRepository<Course> courseRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<CreateCourseCommand, CourseDto>
{
    public async Task<Result<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var existingCourse = await courseRepository.FirstOrDefaultAsync(new GetCourseByNameSpec(request.Name), cancellationToken);
        if (existingCourse is not null) return new Error("course", "Course with this name already exists");

        var skills = await skillRepository.ListAsync(new GetSkillsByIdsSpec([.. request.RelatedSkills]), cancellationToken);
        if (skills.Count != request.RelatedSkills.Count()) return new Error("course", "Invalid skill ids");

        var courseResult = Course.Create(request.Name);
        if (courseResult.IsFail) return courseResult.Error;

        foreach (var tag in request.Tags)
        {
            var result = courseResult.Value.AddTag(tag);
            if (result.IsFail) return result.Error;
        }

        foreach (var skill in skills)
        {
            var result = courseResult.Value.AddRelatedSkill(skill.Id);
            if (result.IsFail) return result.Error;
        }

        await courseRepository.AddAsync(courseResult.Value, cancellationToken);
        return CourseDto.FromEntity(courseResult.Value);
    }
}
