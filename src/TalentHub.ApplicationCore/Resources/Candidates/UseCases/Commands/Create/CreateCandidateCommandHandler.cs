using Humanizer;
using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;

public sealed class CreateCandidateCommandHandler(
    IRepository<Candidate> repository,
    IUserContext userContext
) :
    IRequestHandler<CreateCandidateCommand, Result<CandidateDto>>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateCandidateCommand input,
        CancellationToken cancellationToken)
    {
        Result<User> userResult = await userContext.GetCurrentAsync();
        if (userResult.IsFail)
        {
            return userResult.Error;
        }

        var candidate = new Candidate(
            input.Name,
            userResult.Value.Id,
            input.Phone,
            input.BirthDate,
            input.Address,
            input.InstagramUrl,
            input.LinkedinUrl,
            input.GithubUrl,
            input.ExpectedRemuneration,
            input.Summary
        );

        foreach (string hobbie in input.Hobbies)
        {
            if (candidate.AddHobbie(hobbie) is
                {
                    IsFail: true,
                    Error: var error
                })
            {
                return error;
            }
        }

        foreach (string desiredWorkplaceType in input.DesiredWorkplaceTypes)
        {
            if (!Enum.TryParse(desiredWorkplaceType.Pascalize(), true, out WorkplaceType workplaceType))
            { 
                return Error.BadRequest($"{desiredWorkplaceType} is not valid workplace type");
            }

            if (candidate.AddDesiredWorkplaceType(workplaceType) is
                {
                    IsFail: true,
                    Error: var error
                })
            { 
                return error; 
            }
        }

        foreach (string desiredJobType in input.DesiredJobTypes)
        {
            if (!Enum.TryParse(desiredJobType.Pascalize(), true, out JobType jobType))
            {
                return Error.BadRequest($"{desiredJobType} is not valid job type");
            }

            if (candidate.AddDesiredJobType(jobType) is
                {
                    IsFail: true,
                    Error: var error
                })
            { 
                return error; 
            }
        }

        _ = await repository.AddAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
