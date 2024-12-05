using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Delete;

public sealed class DeleteCompanySectorCommandHandler(
    IRepository<CompanySector> repository
) : ICommandHandler<DeleteCompanySectorCommand>
{
    public async Task<Result> Handle(DeleteCompanySectorCommand request, CancellationToken cancellationToken)
    {
        CompanySector? companySector = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (companySector is null)
        {
            return Error.NotFound("company_sector");
        }

        await repository.DeleteAsync(companySector, cancellationToken);

        return Result.Ok();
    }
}
