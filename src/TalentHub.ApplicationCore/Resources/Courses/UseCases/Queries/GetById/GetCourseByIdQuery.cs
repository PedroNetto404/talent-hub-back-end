using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;

namespace TalentHub.ApplicationCore.Resources.Courses.UseCases.Queries.GetById;

public sealed record GetCourseByIdQuery(Guid CourseId) : IQuery<CourseDto>;