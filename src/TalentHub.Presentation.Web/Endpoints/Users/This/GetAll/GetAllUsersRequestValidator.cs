using FluentValidation;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Users.This.GetAll;

public sealed class GetAllUsersRequestValidator : PageRequestValidator<GetAllUsersRequest>
{
    private static readonly string[] AllowedSortFields = ["username", "email", "role"];
    protected override string[] SortableFields => AllowedSortFields;
    
    public GetAllUsersRequestValidator()
    {
        RuleFor(p => p.UsernameLike)
            .NotNull()
            .NotEmpty()
            .When(p => p is not null);
        
        RuleFor(p => p.EmailLike)
            .NotNull()
            .NotEmpty()
            .When(p => p is not null);
        
        RuleFor(p => p.Role)
            .NotNull()
            .NotEmpty()
            .Must(r => Role.TryFromName(r, true, out _))
            .When(p => p is not null);
    }
}
