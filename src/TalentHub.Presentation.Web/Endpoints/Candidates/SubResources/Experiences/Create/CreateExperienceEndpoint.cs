using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.CreateExperience;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Create;

public sealed class CreateExperienceEndpoint : Ep.Req<CreateExperienceRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("{type:alpha}");
        Version(1);
        Group<ExperiencesSubGroup>();
        Validator<CreateExperienceRequestValidator>();
    }

    public override Task HandleAsync(
        CreateExperienceRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new CreateExperienceCommand(
            req.CandidateId,
            req.Type,
            req.Start,
            req.End,
            req.IsCurrent,
            req.Activities,
            req.ExpectedGraduation,
            req.EducationalLevel,
            req.ProgressStatus,
            req.CurrentSemester,
            req.AcademicEntities ?? [],
            req.CourseId,
            req.UniversityId,
            req.Position,
            req.Description,
            req.Company,
            req.ProfessionalLevel
        ),
        ct);
}
