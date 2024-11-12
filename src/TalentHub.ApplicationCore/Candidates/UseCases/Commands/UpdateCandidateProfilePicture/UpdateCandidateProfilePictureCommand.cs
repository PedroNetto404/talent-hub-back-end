using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;

public sealed record UpdateCandidateProfilePictureCommand(
    Guid CandidateId,
    Stream File,
    string ContentType
) : ICommand<CandidateDto>;
