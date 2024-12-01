namespace TalentHub.ApplicationCore.Core.Results;

public record Error(string Code, string Message)
{
    public static Error NotFound(string resource) =>
        new("not_found", $"{resource} not found");

    public static Error BadRequest(string message) =>
        new("bad_request", message);
}
