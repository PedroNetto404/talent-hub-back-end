using FluentValidation;
using FluentValidation.Results;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.UpdateProfilePicture;

// public sealed class UpdateProfilePictureCommandValidator : AbstractValidator<UpdateProfilePictureCommand>
// {
//     public UpdateProfilePictureCommandValidator()
//     {
//         RuleFor(p => p.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
//         RuleFor(p => p.ContentType).NotEmpty().NotNull();
//         RuleFor(p => p.File).Custom((file, context) =>
//         {
//             if (file.Length > 0)
//             {
//                 context.AddFailure(new ValidationFailure()
//                 {
//                     ErrorMessage = "Empty File"
//                 });
//             }
//         });
//     }
// }
