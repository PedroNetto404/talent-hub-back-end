using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Skills.UseCases.Commands.CreateSkill;
using TalentHub.ApplicationCore.Skills.UseCases.Commands.DeleteSkill;
using TalentHub.ApplicationCore.Skills.UseCases.Commands.UpdateSkill;
using TalentHub.ApplicationCore.Skills.UseCases.Dtos;
using TalentHub.ApplicationCore.Skills.UseCases.Queries.GetAllSkills;
using TalentHub.ApplicationCore.Skills.UseCases.Queries.GetSkillById;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[ApiController]
[Route("api/skills")]
public sealed class SkillController(ISender sender) : ApiController(sender)
{
    [HttpGet("{skillId:guid}")]
    public Task<IActionResult> GetByIdAsync(Guid skillId, CancellationToken cancellationToken = default) =>
        HandleAsync<SkillDto>(
            new GetSkillByIdQuery(skillId),
            cancellationToken: cancellationToken
        );

    [HttpGet]
    public Task<IActionResult> GetAllAsync(PagedRequest request, CancellationToken cancellationToken) =>
       HandleAsync<PagedResponse<SkillDto>>(
            new GetAllSkillsQuery(
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortAscending
            ),
            cancellationToken: cancellationToken
       );

    [HttpPost]
    public Task<IActionResult> CreateAsync(
        CreateSkillRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new CreateSkillCommand(
                request.Name,
                request.Type,
                [.. request.Tags],
                false
            ),
            cancellationToken: cancellationToken
        );

    [HttpPut("{id:guid}")]
    public Task<IActionResult> UpdateAsync(
        Guid id,
        UpdateSkillRequest request,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new UpdateSkillCommand(
                id,
                request.Name,
                request.Tags
            ),
            onSuccess: NoContent,
            cancellationToken: cancellationToken
        );

    [HttpDelete("{id:guid}")]
    public Task<IActionResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken
    ) =>
        HandleAsync(
            new DeleteSkillCommand(id),
            onSuccess: NoContent,
            cancellationToken: cancellationToken
        );
}
