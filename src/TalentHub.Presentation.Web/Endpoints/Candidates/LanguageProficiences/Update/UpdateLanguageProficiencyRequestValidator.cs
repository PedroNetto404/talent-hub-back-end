using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Update;

public sealed class UpdateLanguageProficiencyRequestValidator : AbstractValidator<UpdateLanguageProficiencyRequest>
{
    public UpdateLanguageProficiencyRequestValidator()
    {
        string allowedProficiences = string.Join(", ", Enum.GetValues<Proficiency>().Select(p => p.ToString().Underscore()));

        RuleFor(p => p.ListeningLevel)
            .NotNull()
            .NotEmpty()
            .Custom((listeningLevel, ctx) =>
            {
                if (!Enum.TryParse<Proficiency>(listeningLevel.Pascalize(), out _))
                {
                    ctx.AddFailure("ListeningLevel", $"ListeningLevel must be one of: {allowedProficiences}");
                }
            });

        RuleFor(p => p.WritingLevel)
            .NotNull()
            .NotEmpty()
            .Custom((writingLevel, ctx) =>
            {
                if (!Enum.TryParse<Proficiency>(writingLevel.Pascalize(), out _))
                {
                    ctx.AddFailure("WritingLevel", $"WritingLevel must be one of: {allowedProficiences}");
                }
            });

        RuleFor(p => p.SpeakingLevel)
            .NotNull()
            .NotEmpty()
            .Custom((writingLevel, ctx) =>
            {
                if (!Enum.TryParse<Proficiency>(writingLevel.Pascalize(), out _))
                {
                    ctx.AddFailure("SpeakingLevel", $"SpeakingLevel must be one of: {allowedProficiences}");
                }
            });
    }
}
