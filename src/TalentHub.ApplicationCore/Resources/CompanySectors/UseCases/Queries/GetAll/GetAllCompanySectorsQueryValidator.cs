using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetAll;

public sealed class GetAllCompanySectorsQueryValidator : AbstractValidator<GetAllCompanySectorsQuery>
{
    public GetAllCompanySectorsQueryValidator()
    {
        RuleFor(x => x.Limit).GreaterThan(0);
        RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
    }
}
