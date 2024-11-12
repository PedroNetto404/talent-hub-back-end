using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Jobs.Enums;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidate;

public sealed class UpdateCandidateCommandHandler(
    IRepository<Candidate> repository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCandidateCommand, CandidateDto>
{
    private static readonly Dictionary<string, Func<Candidate, object, Result>> UpdateFunctions = new()
    {
        [nameof(UpdateCandidateCommand.Name)] = (candidate, value) => candidate.UpdateName((string)value),
        [nameof(UpdateCandidateCommand.Phone)] = (candidate, value) => candidate.UpdatePhone((string)value),
        [nameof(UpdateCandidateCommand.Address)] = (candidate, value) => candidate.UpdateAddress((Address)value),
        [nameof(UpdateCandidateCommand.InstagramUrl)] = (candidate, value) => candidate.UpdateInstagramUrl((string)value),
        [nameof(UpdateCandidateCommand.LinkedinUrl)] = (candidate, value) => candidate.UpdateLinkedinUrl((string)value),
        [nameof(UpdateCandidateCommand.GithubUrl)] = (candidate, value) => candidate.UpdateGithubUrl((string)value),
        [nameof(UpdateCandidateCommand.Summary)] = (candidate, value) => candidate.UpdateSummary((string)value),
        [nameof(UpdateCandidateCommand.DesiredJobTypes)] = (candidate, value) =>
        {
            candidate.ClearDesiredJobTypes();
            foreach(var desiredJobType in (JobType[])value)
                if(candidate.AddDesiredJobType(desiredJobType) is 
                {
                    IsFail: true,
                    Error: var error
                }) return error;

            return Result.Ok();
        },
        [nameof(UpdateCandidateCommand.DesiredJobTypes)] = (candidate, value) =>
        {
            candidate.ClearDesiredWorkplaceTypes();
            foreach(var desiredWorkplaceType in (WorkplaceType[])value)
                if(candidate.AddDesiredWorkplaceType(desiredWorkplaceType) is 
                {
                    IsFail: true,
                    Error: var error
                }) return error;

            return Result.Ok();
        },
      [nameof(UpdateCandidateCommand.Hobbies)] = (candidate, value) =>
        {
            candidate.ClearHobbies();
            foreach(var hobbie in (string[])value)
                if(candidate.AddHobbie(hobbie) is 
                {
                    IsFail: true,
                    Error: var error
                }) return error;

            return Result.Ok();
        },
    };

    public async Task<Result<CandidateDto>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await repository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return Error.Displayable("not_found", "candidate not found");

        var result = UpdateFunctions.Select(entry =>
        {
            var (prop, func) = entry;
            
            var value = typeof(UpdateCandidateCommand)
                .GetProperty(prop)!
                .GetValue(request)!;

            return func(candidate, value);
        }).FirstOrDefault(r => r.IsFail, Result.Ok());
        if (result.IsFail) return result.Error;

        if(request.ResumeFile is not null)
        {
            var resumeFileName = candidate.ResumeFileName;
            await fileStorage.DeleteAsync(resumeFileName, cancellationToken);

            var resumeUrl = await fileStorage.SaveAsync(
                request.ResumeFile!,
                resumeFileName,
                "application/pdf",
                cancellationToken
            );

            if(candidate.SetResumeUrl(resumeUrl) is 
            {
                IsFail: true,
                Error: var err
            }) return err;
        }

        await repository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}