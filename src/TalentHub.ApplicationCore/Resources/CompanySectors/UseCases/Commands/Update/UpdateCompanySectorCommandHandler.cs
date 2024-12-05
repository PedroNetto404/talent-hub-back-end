using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Update;

public sealed class UpdateCompanySectorCommandHandler(
    IRepository<CompanySector> repository
) : ICommandHandler<UpdateCompanySectorCommand, CompanySectorDto>
{
    public async Task<Result<CompanySectorDto>> Handle(UpdateCompanySectorCommand request, CancellationToken cancellationToken)
    {
        CompanySector? companySector = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (companySector is null)
        {
            return Error.NotFound("company_sector");
        }

        if (companySector.ChangeName(request.Name) is
            {
                IsFail: true,
                Error: var err
            })
        {
            return err;
        }

        await repository.UpdateAsync(companySector, cancellationToken);

        return CompanySectorDto.FromEntity(companySector);
    }
}
