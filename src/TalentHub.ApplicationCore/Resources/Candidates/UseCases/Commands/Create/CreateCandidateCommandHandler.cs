using Humanizer;
using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Create;

public sealed class CreateCandidateCommandHandler(
    IRepository<Candidate> candidateRepository,
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
            return Error.Unauthorized("invalid authentication state");
        }

        Candidate? existingCandidate = await candidateRepository.FirstOrDefaultAsync(
            new GetCandidateByUserOrPhoneSpec(user.Id, input.Phone),
            cancellationToken
        );
        if (existingCandidate is not null)
        {
            return Error.InvalidInput("candidate already exists");
        }

        Result<Candidate> maybeCandidate = Candidate.Create(
            input.Name,
            input.AutoMatchEnabled,
            user.Id,
            input.Phone,
            input.BirthDate,
            input.Address,
            input.InstagramUrl,
            input.LinkedInUrl,
            input.GitHubUrl,
            input.ExpectedRemuneration,
            input.Summary
        );
        if (maybeCandidate is not { IsOk: true, Value: Candidate candidate })
        {
            return maybeCandidate.Error;
        }

        foreach (string hobbie in input.Hobbies)
        {
            if (candidate.AddHobbie(hobbie) is { IsFail: true, Error: var error })
            {
                return error;
            }
        }

        foreach (string desiredWorkplaceType in input.DesiredWorkplaceTypes)
        {
            if (!Enum.TryParse(desiredWorkplaceType.Pascalize(), true, out WorkplaceType workplaceType))
            {
                return Error.InvalidInput($"{desiredWorkplaceType} is not valid WorkplaceType");
            }

            if (candidate.AddDesiredWorkplaceType(workplaceType) is { IsFail: true, Error: var error })
            {
                return error;
            }
        }

        foreach (string desiredJobType in input.DesiredJobTypes)
        {
            if (!Enum.TryParse(desiredJobType.Pascalize(), true, out JobType jobType))
            {
                return Error.InvalidInput($"{desiredJobType} is not valid WorkplaceType");
            }

            if (candidate.AddDesiredJobType(jobType) is { IsFail: true, Error: var error })
            {
                return error;
            }
        }

        _ = await candidateRepository.AddAsync(maybeCandidate, cancellationToken);
        return CandidateDto.FromEntity(maybeCandidate);
    }
}
