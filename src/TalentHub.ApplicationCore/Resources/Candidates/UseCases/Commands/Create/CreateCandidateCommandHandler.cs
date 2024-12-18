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
        User? user = await userContext.GetCurrentAsync(cancellationToken);
        if (user is null)
        {
            return Error.Unauthorized();
        }

        Result<Address> maybeAddress = Address.Create(
            input.AddressStreet,
            input.AddressNumber,
            input.AddressNeighborhood,
            input.AddressCity,
            input.AddressState,
            input.AddressCountry,
            input.AddressZipCode
        );
        if (maybeAddress is { IsFail: true, Error: var error })
        {
            return error;
        }

        Result<Candidate> maybeCandidate = Candidate.Create(
            input.Name,
            input.AutoMatchEnabled,
            user.Id,
            input.Phone,
            input.BirthDate,
            maybeAddress.Value,
            input.InstagramUrl,
            input.LinkedinUrl,
            input.GithubUrl,
            input.ExpectedRemuneration,
            input.Summary
        );
        if (maybeCandidate is not { IsOk: true, Value: Candidate candidate })
        {
            return maybeCandidate.Error;
        }

        var result = Result.FailEarly([
            .. input.Hobbies.Select<string, Func<Result>>((hobbie) => () => candidate.AddHobbie(hobbie)),
            .. input.DesiredWorkplaceTypes.Select<string, Func<Result>>((desiredWorkplaceType) => () =>
            {
                if (!Enum.TryParse(desiredWorkplaceType.Pascalize(), true, out WorkplaceType workplaceType))
                {
                    return Error.BadRequest($"{desiredWorkplaceType} is not valid workplace type");
                }

                return candidate.AddDesiredWorkplaceType(workplaceType);
            }),
            .. input.DesiredJobTypes.Select<string, Func<Result>>((desiredJobType) => () =>
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
