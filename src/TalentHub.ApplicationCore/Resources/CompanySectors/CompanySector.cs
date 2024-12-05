using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.CompanySectors;

public sealed class CompanySector : AggregateRoot
{
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected CompanySector() { }
#pragma warning restore CS0628 // New protected member declared in sealed type

    private CompanySector(string name) => Name = name;

    public static Result<CompanySector> Create(string name)
    {
        if(
            Result.FailIfIsNullOrWhiteSpace(name, "invalid company sector name") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        return new CompanySector(name);
    }

    public string Name { get; private set; }

    public Result ChangeName(string name)
    {
        if(Result.FailIfIsNullOrWhiteSpace(name, "invalid company sector name") is
        {
            IsFail: true,
            Error: var err
        })
        {
            return err;
        }

        Name = name;

        return Result.Ok();
    }
}
