namespace TalentHub.ApplicationCore.Core.Results;

public record Error(string Code, string Message, bool CanBeDisplayed = false)
{
    public static Error Displayable(string code, string message) =>
        new(code, message, true);
}