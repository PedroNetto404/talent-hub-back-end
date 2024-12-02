using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.CreateSkill;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.DeleteSkill;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.UpdateSkill;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetAllSkills;
using TalentHub.ApplicationCore.Resources.Skills.UseCases.Queries.GetSkillById;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Binders;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[ApiController]
[Route("api/skills")]
[Authorize(Roles = "admin")]
public sealed class SkillController(ISender sender) : ApiController(sender)
{
    [HttpGet("{skillId:guid}")]
    public Task<IActionResult> GetByIdAsync(
        Guid skillId,
        CancellationToken cancellationToken = default
    ) => HandleAsync<SkillDto>(
        new GetSkillByIdQuery(skillId),
        cancellationToken: cancellationToken
    );

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(
        PagedRequest request,
        [ModelBinder(typeof(SplitQueryStringBinder)), FromQuery(Name = "skill_id_in")]
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken
    ) => HandleAsync<PagedResponse>(
        new GetAllSkillsQuery(
            [.. ids],
            request.Limit,
            request.Offset,
            request.SortBy,
            request.Ascending
        ),
        cancellationToken: cancellationToken
    );

    [HttpPost]
    public Task<IActionResult> CreateAsync(
        CreateSkillRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateSkillCommand(
            request.Name,
            request.Type,
            [.. request.Tags]
        ),
        cancellationToken: cancellationToken,
        onSuccess: dto => Created($"api/skills/{dto.Id}", dto)
    );

    [HttpPut("{id:guid}")]
    public Task<IActionResult> UpdateAsync(
        Guid id,
        UpdateSkillRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
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
    ) => HandleAsync(
        new DeleteSkillCommand(id),
        onSuccess: NoContent,
        cancellationToken: cancellationToken
    );
}
