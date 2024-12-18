using System.Text.Json.Serialization;

namespace TalentHub.ApplicationCore.Core.Results;

public record Error(string Code, string Message)
{
    public static Error NotFound(string resource, bool expose = true) =>
        CreateError("not_found", $"{resource} not found", expose);

    public static Error BadRequest(string message, bool expose = true) =>
        CreateError("bad_request", message, expose);

    public static Error Unexpected(string message, bool expose = true) =>
        CreateError("unexpected", message, expose);

    public static Error Unauthorized(
        string message = "check your credentials", 
        bool expose = true
    ) => CreateError("unauthorized", message, expose);

    private static Error CreateError(string code, string message, bool expose = true) =>
        new(code, message)
        {
            Expose = expose
        };

    [JsonIgnore]
    public bool Expose { get; init; }
}
