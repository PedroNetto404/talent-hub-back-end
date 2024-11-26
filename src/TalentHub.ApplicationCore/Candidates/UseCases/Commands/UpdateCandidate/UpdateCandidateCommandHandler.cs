using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Skills;
using Ardalis.Specification;
using TalentHub.ApplicationCore.Skills.Specs;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidate;

public sealed class UpdateCandidateCommandHandler(
    IRepository<Candidate> candidateRepository,
    IRepository<Skill> skillRepository
) : ICommandHandler<UpdateCandidateCommand>
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
            foreach (var desiredJobType in (JobType[])value)
                if (candidate.AddDesiredJobType(desiredJobType) is
                    {
                        IsFail: true,
                        Error: var error
                    }) return error;

            return Result.Ok();
        },
        [nameof(UpdateCandidateCommand.DesiredJobTypes)] = (candidate, value) =>
        {
            candidate.ClearDesiredWorkplaceTypes();
            foreach (var desiredWorkplaceType in (WorkplaceType[])value)
                if (candidate.AddDesiredWorkplaceType(desiredWorkplaceType) is
                    {
                        IsFail: true,
                        Error: var error
                    }) return error;

            return Result.Ok();
        },
        [nameof(UpdateCandidateCommand.Hobbies)] = (candidate, value) =>
          {
              candidate.ClearHobbies();
              foreach (var hobbie in (string[])value)
                  if (candidate.AddHobbie(hobbie) is
                      {
                          IsFail: true,
                          Error: var error
                      }) return error;

              return Result.Ok();
          },
    };

    public async Task<Result> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return NotFoundError.Value;

        var result = UpdateFunctions.Select(entry =>
        {
            var (prop, func) = entry;

            var value = typeof(UpdateCandidateCommand)
                .GetProperty(prop)!
                .GetValue(request)!;

            return func(candidate, value);
        }).FirstOrDefault(r => r.IsFail, Result.Ok());
        if (result.IsFail) return result.Error;

        var skills = await skillRepository.ListAsync(
            new GetSkillsByIdsSpec([.. candidate.Skills.Select(p => p.SkillId)]), 
            cancellationToken);

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}