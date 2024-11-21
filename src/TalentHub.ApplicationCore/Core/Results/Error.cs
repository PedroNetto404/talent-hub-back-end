namespace TalentHub.ApplicationCore.Core.Results;

public record Error(string Code, string Message);

public record NotFoundError : Error
{
    private NotFoundError() : base("not_found", "resource not found") { }

    public static readonly Error Value = new NotFoundError(); 
}