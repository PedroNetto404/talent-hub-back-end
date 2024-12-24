using System.Text.Json.Serialization;

namespace TalentHub.ApplicationCore.Core.Results;

public record Error(string Code, string Message)
{
    public static Error NotFound(string resource) =>
        new("not_found", $"{resource} not found");

    public static Error BadRequest(string message) =>
        new("bad_request", message);

    public static Error Unexpected(string message) =>
        new("unexpected", message);

    public static Error Unauthorized(
        string message = "check your credentials"
    ) => new("unauthorized", message);
}
