using MediatR;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Candidates.Specs;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;

public sealed class CreateCandidateCommandHandler(
    IRepository<Candidate> repository,
    IFileStorage fileStorage
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
            return Error.Displayable("candidate", "candidate already exists");

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

        if (input.ResumeFile is not null)
        {
            var resumeUrl = await fileStorage.SaveAsync(
                input.ResumeFile,
                candidate.ResumeFileName,
                "application/pdf",
                cancellationToken);

            candidate.SetResumeUrl(resumeUrl);
        }

        foreach (var hobbie in input.Hobbies)
            if(candidate.AddHobbie(hobbie) is 
            {
                IsFail: true,
                Error: var error
            }) return error;

        foreach(var desiredWorkplaceType in input.DesiredWorkplaceTypes)
            if(candidate.AddDesiredWorkplaceType(desiredWorkplaceType) is 
            {
                IsFail: true,
                Error: var error
            }) return error;

        foreach(var desiredJobType in input.DesiredJobTypes)
            if(candidate.AddDesiredJobType(desiredJobType) is 
            {
                IsFail: true,
                Error: var error
            }) return error;

        _ = await repository.AddAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
