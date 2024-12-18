using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public sealed record Address
{
    public string Street { get; init; }
    public string Number { get; init; }
    public string Neighborhood { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    private Address(
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string country,
        string zipCode
    )
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Result<Address> Create(
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string country,
        string zipCode)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(street), "invalid street"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(number), "invalid number"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(neighborhood), "invalid neighborhood"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(city), "invalid city"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(state), "invalid state"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(country), "invalid country"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(zipCode), "invalid zipCode")
            ) is { IsFail: true, Error: var error })
        {
            return error;
        }
        
        return new Address(
            street,
            number,
            neighborhood,
            city,
            state,
            country,
            zipCode
        );
    }
}

