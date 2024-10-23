using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Controllers;

[Route("candidates/{candidateId:guid}/educational_experiences")]
public sealed class CandidateEducationalExperienceController(
    ISender sender
) : ApiController
{
}

[Route("candidates/{candidateId:guid}/professional_experiences")]
public sealed class CandidateProfessionalExperienceController(
    ISender sender
) : ApiController
{
}

