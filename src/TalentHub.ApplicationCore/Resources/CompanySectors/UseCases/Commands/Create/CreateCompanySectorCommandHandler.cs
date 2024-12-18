using Ardalis.Specification;
using FluentValidation;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Resources.CompanySectors.Specs;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Create;

public sealed class CreateCompanySectorCommandHandler(
    IRepository<CompanySector> repository
) : ICommandHandler<CreateCompanySectorCommand, CompanySectorDto>
{
    public async Task<Result<CompanySectorDto>> Handle(
        CreateCompanySectorCommand request,
        CancellationToken cancellationToken
    )
    {
        CompanySector? existing = await repository.FirstOrDefaultAsync(
            new GetCompanySectorByName(request.Name),
            cancellationToken
        );
        if (existing is not null)
        {
            return Error.BadRequest("Company sector already exists");
        }

        Result<CompanySector> maybeCompanySector = CompanySector.Create(request.Name);
        if (maybeCompanySector.IsFail)
        {
            return maybeCompanySector.Error;
        }
        CompanySector companySector = maybeCompanySector.Value;

        await repository.AddAsync(companySector, cancellationToken);

        return CompanySectorDto.FromEntity(companySector);
    }
}
