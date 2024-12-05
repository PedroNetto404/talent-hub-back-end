using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Delete;

public sealed class DeleteCompanyCommandHandler(
    IRepository<Company> companyRepository
) : ICommandHandler<DeleteCompanyCommand>
{
    public async Task<Result> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if (company is null)
        {
            return Error.NotFound("company");
        }

        await companyRepository.DeleteAsync(company, cancellationToken);

        return Result.Ok();
    }
}
