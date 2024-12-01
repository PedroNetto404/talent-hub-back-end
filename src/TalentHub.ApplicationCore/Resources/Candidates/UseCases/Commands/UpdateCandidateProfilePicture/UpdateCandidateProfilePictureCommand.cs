using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;

public sealed record UpdateCandidateProfilePictureCommand(
    Guid CandidateId,
    Stream File,
    string ContentType
) : ICommand<CandidateDto>;
