using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Update;

public sealed class UpdateCandidateCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<UpdateCandidateCommand>
{
    public async Task<Result> Handle(
        UpdateCandidateCommand request,
        CancellationToken cancellationToken
    )
    {
        (
            Guid candidateId,
            string name,
            string phone,
            Address address,
            IEnumerable<string> desiredWorkplaceTypes,
            IEnumerable<string> desiredJobTypes,
            decimal? expectedRemuniration,
            string? instagramUrl,
            string? linkedinUrl,
            string? githubUrl,
            string? summary,
            IEnumerable<string> hobbies
        ) = request;

        Candidate? candidate = await candidateRepository.GetByIdAsync(candidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        var result = Result.FailEarly(
            () => candidate.ChangeName(name),
            () => candidate.ChangePhone(phone),
            () => candidate.ChangeAddress(address),
            () => candidate.ChangeSummary(summary),
            () => candidate.ChangeExpectedRemuneration(expectedRemuniration),
            () => candidate.ChangeLinkedinUrl(linkedinUrl),
            () => candidate.ChangeGithubUrl(githubUrl),
            () => candidate.ChangeInstagramUrl(instagramUrl),
            () =>
            {
                candidate.ClearDesiredWorkplaceTypes();
                foreach (string desiredWorkplaceType in desiredWorkplaceTypes)
                {
                    if (!Enum.TryParse(desiredWorkplaceType, true, out WorkplaceType workplaceType))
                    {
                        return Error.BadRequest($"{desiredWorkplaceType} is not valid workplace type");
                    }

                    if (candidate.AddDesiredWorkplaceType(workplaceType) is { IsFail: true } resultWorkplaceType && resultWorkplaceType.Error is var errWorkplace)
                    {
                        return errWorkplace;
                    }
                }

                return Result.Ok();
            },
            () =>
            {
                candidate.ClearDesiredJobTypes();
                foreach (string desiredJobType in desiredJobTypes)
                {
                    if (!Enum.TryParse(desiredJobType, true, out JobType jobType))
                    {
                        return Error.BadRequest($"{desiredJobType} is not valid job type");
                    }

                    if (candidate.AddDesiredJobType(jobType) is { IsFail: true } resultJobType && resultJobType.Error is var errJobType)
                    {
                        return errJobType;
                    }
                }

                return Result.Ok();
            },
            () =>
            {
                candidate.ClearHobbies();
                foreach (string hobbie in hobbies)
                {
                    if (candidate.AddHobbie(hobbie) is { IsFail: true, Error: var hobbieError })
                    {
                        return hobbieError;
                    }
                }

                return Result.Ok();
            }
        );
        if(result.IsFail)
        {
            return result;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
