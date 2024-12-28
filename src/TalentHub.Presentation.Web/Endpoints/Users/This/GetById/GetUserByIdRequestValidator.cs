using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Users.This.GetById;

public sealed class GetUserByIdRequestValidator :
    AbstractValidator<GetUserByIdRequest>
{
  public GetUserByIdRequestValidator()
  {
    RuleFor(x => x.UserId).NotEmpty().NotNull().NotEqual(Guid.Empty);
  }
}
