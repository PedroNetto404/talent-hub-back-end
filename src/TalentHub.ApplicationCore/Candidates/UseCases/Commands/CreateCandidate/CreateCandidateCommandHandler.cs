using MediatR;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Specs;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;

public sealed class CreateCandidateCommandHandler(
    IRepository<Candidate> repository
) :
    IRequestHandler<CreateCandidateCommand, Result<CandidateDto>>
{
    public async Task<Result<CandidateDto>> Handle(
        CreateCandidateCommand input,
        CancellationToken cancellationToken)
    {
        var existingCandidate = await repository.FirstOrDefaultAsync(
            new CandidateByEmailSpec(input.Email),
            cancellationToken);
        if (existingCandidate is not null)
            return new Error("candidate", "candidate already exists");

        var candidate = new Candidate(
            input.Name,
            input.Email,
            input.Phone,
            input.BirthDate,
            input.Address,
            input.InstagramUrl,
            input.LinkedinUrl,
            input.GithubUrl,
            input.ExpectedRemuneration,
            input.Summary
        );

        foreach (var hobbie in input.Hobbies)
            if (candidate.AddHobbie(hobbie) is
                {
                    IsFail: true,
                    Error: var error
                }) return error;

        foreach (var desiredWorkplaceType in input.DesiredWorkplaceTypes)
            if (candidate.AddDesiredWorkplaceType(desiredWorkplaceType) is
                {
                    IsFail: true,
                    Error: var error
                }) return error;

        foreach (var desiredJobType in input.DesiredJobTypes)
            if (candidate.AddDesiredJobType(desiredJobType) is
                {
                    IsFail: true,
                    Error: var error
                }) return error;

        _ = await repository.AddAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate, []);
    }
}
