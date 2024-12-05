using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetById;

public sealed class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanySectorByIdQuery>
{
    public GetCompanyByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
    }
}
