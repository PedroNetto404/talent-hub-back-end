using Humanizer;
using MediatR;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Users;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;

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
        { return userResult.Error; }

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
            { return error; }
        }

        foreach (string desiredWorkplaceType in input.DesiredWorkplaceTypes)
        {
            if (!Enum.TryParse(desiredWorkplaceType.Pascalize(), true, out WorkplaceType workplaceType))
            { return new Error("workplace_type", "Invalid workplace type"); }

            if (candidate.AddDesiredWorkplaceType(workplaceType) is
                {
                    IsFail: true,
                    Error: var error
                })
            { return error; }
        }

        foreach (string desiredJobType in input.DesiredJobTypes)
        {
            if (!Enum.TryParse(desiredJobType.Pascalize(), true, out JobType jobType))
            {
                return new Error("job_type", "Invalid job type");
            }

            if (candidate.AddDesiredJobType(jobType) is
                {
                    IsFail: true,
                    Error: var error
                })
            { return error; }
        }

        _ = await repository.AddAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
