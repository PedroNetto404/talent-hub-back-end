using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Jobs.Enums;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.Update;

public sealed class UpdateCandidateCommandHandler(
    IRepository<Candidate> candidateRepository
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
            foreach (JobType desiredJobType in (JobType[])value)
            {
                if (candidate.AddDesiredJobType(desiredJobType) is
                    {
                        IsFail: true,
                        Error: var error
                    })
                { return error; }
            }

            return Result.Ok();
        },
        [nameof(UpdateCandidateCommand.DesiredWorkplaceTypes)] = (candidate, value) =>
        {
            candidate.ClearDesiredWorkplaceTypes();
            foreach (WorkplaceType desiredWorkplaceType in (WorkplaceType[])value)
            {
                if (candidate.AddDesiredWorkplaceType(desiredWorkplaceType) is
                    {
                        IsFail: true,
                        Error: var error
                    })
                { return error; }
            }

            return Result.Ok();
        },
        [nameof(UpdateCandidateCommand.Hobbies)] = (candidate, value) =>
          {
              candidate.ClearHobbies();
              foreach (string hobbie in (string[])value)
              {
                  if (candidate.AddHobbie(hobbie) is
                      {
                          IsFail: true,
                          Error: var error
                      })
                  { return error; }
              }

              return Result.Ok();
          },
    };

    public async Task<Result> Handle(
        UpdateCandidateCommand request,
        CancellationToken cancellationToken
    )
    {
        Candidate? candidate = await candidateRepository.GetByIdAsync(
            request.CandidateId,
            cancellationToken
        );

        if (candidate is null)
        { return NotFoundError.Value; }

        Result result = UpdateFunctions.Select(entry =>
        {
            (string prop, Func<Candidate, object, Result> func) = entry;

            object value = typeof(UpdateCandidateCommand)
                .GetProperty(prop)!
                .GetValue(request)!;

            return func(candidate, value);
        }).FirstOrDefault(r => r.IsFail, Result.Ok());

        if (result.IsFail)
        { return result.Error; }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}