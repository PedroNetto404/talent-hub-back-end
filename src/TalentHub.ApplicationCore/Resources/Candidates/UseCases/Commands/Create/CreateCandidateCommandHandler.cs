using Humanizer;
using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;

public sealed class CreateCandidateCommandHandler(
    IRepository<Candidate> repository,
    IUserContext userContext
) :
    IRequestHandler<CreateCandidateCommand, Result<CandidateDto>>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateCandidateCommand input,
        CancellationToken cancellationToken
    )
    {
        (
            string name,
            bool autoMatchEnabled,
            string phone,
            DateOnly birthDate,
            Address address,
            IEnumerable<string> desiredJobTypes,
            IEnumerable<string> desiredWorkplaceTypes,
            string? summary,
            string? githubUrl,
            string? instagramUrl,
            string? linkedinUrl,
            decimal? expectedRemuneration,
            IEnumerable<string> hobbies
        ) = input;

        Result<User> maybeUser = await userContext.GetCurrentAsync(cancellationToken);
        if (maybeUser.IsFail)
        {
            return maybeUser.Error;
        }

        Result<Candidate> maybeCandidate = Candidate.Create(
            name,
            autoMatchEnabled,
            maybeUser.Value.Id,
            phone,
            birthDate,
            address,
            instagramUrl,
            linkedinUrl,
            githubUrl,
            expectedRemuneration,
            summary
        );
        if (maybeCandidate is not { IsOk: true, Value: Candidate candidate })
        {
            return maybeCandidate.Error;
        }

        var result = Result.FailEarly([
            .. hobbies.Select<string, Func<Result>>((hobbie) => () => candidate.AddHobbie(hobbie)),
            .. desiredWorkplaceTypes.Select<string, Func<Result>>((desiredWorkplaceType) => () =>
            {
                if (!Enum.TryParse(desiredWorkplaceType.Pascalize(), true, out WorkplaceType workplaceType))
                {
                    return Error.BadRequest($"{desiredWorkplaceType} is not valid workplace type");
                }

                return candidate.AddDesiredWorkplaceType(workplaceType);
            }),
            .. desiredJobTypes.Select<string, Func<Result>>((desiredJobType) => () =>
            {
                if (!Enum.TryParse(desiredJobType.Pascalize(), true, out JobType jobType))
                {
                    return Error.BadRequest($"{desiredJobType} is not valid job type");
                }

                return candidate.AddDesiredJobType(jobType);
            })
        ]);
        if (result.IsFail)
        {
            return result.Error;
        }

        _ = await repository.AddAsync(maybeCandidate, cancellationToken);
        return CandidateDto.FromEntity(maybeCandidate);
    }
}
