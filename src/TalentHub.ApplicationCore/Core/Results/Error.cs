namespace TalentHub.ApplicationCore.Core.Results;

public record Error(string Code, string Message)
{
    public const string NotFoundCode = "not_found";
    public const string InvalidInputCode = "invalid_input";
    public const string UnexpectedCode = "unexpected";
    public const string UnauthorizedCode = "unauthorized";


    public static Error NotFound(string resource) =>
        new(NotFoundCode, $"{resource} not found");

    public static Error InvalidInput(string message) =>
        new(InvalidInputCode, message);

    public static Error Unexpected(string message) =>
        new(UnexpectedCode, message);

    public static Error Unauthorized(
        string message = "check your credentials"
    ) => new(UnauthorizedCode, message);
}
