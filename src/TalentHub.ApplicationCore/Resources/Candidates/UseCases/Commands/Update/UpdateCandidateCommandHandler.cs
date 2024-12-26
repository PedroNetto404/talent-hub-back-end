using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;

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
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        candidate.SetAutoMatchEnabled(request.AutoMatchEnabled);

        var result = Result.FailEarly(
            () => candidate.ChangeName(request.Name),
            () => candidate.ChangePhone(request.Phone),
            () => candidate.ChangeAddress(request.Address),
            () => candidate.ChangeSummary(request.Summary),
            () => candidate.ChangeExpectedRemuneration(request.ExpectedRemuneration),
            () => candidate.ChangeLinkedinUrl(request.LinkedInUrl),
            () => candidate.ChangeGithubUrl(request.GitHubUrl),
            () => candidate.ChangeInstagramUrl(request.InstagramUrl),
            () =>
            {
                candidate.ClearDesiredWorkplaceTypes();
                foreach (string desiredWorkplaceType in request.DesiredWorkplaceTypes)
                {
                    if (!Enum.TryParse(desiredWorkplaceType.Pascalize(), true, out WorkplaceType workplaceType))
                    {
                        return Error.InvalidInput($"{desiredWorkplaceType} is not valid workplace type");
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
                foreach (string desiredJobType in request.DesiredJobTypes)
                {
                    if (!Enum.TryParse(desiredJobType.Pascalize(), true, out JobType jobType))
                    {
                        return Error.InvalidInput($"{desiredJobType} is not valid job type");
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
                foreach (string hobbie in request.Hobbies)
                {
                    if (candidate.AddHobbie(hobbie) is { IsFail: true, Error: var hobbieError })
                    {
                        return hobbieError;
                    }
                }

                return Result.Ok();
            }
        );
        if (result.IsFail)
        {
            return result;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
