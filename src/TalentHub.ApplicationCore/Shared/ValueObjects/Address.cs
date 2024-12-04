using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public record Address
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
        string zipCode)
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
                () => Result.FailIfIsNullOrWhiteSpace(street, "invalid street"),
                () => Result.FailIfIsNullOrWhiteSpace(number, "invalid number"),
                () => Result.FailIfIsNullOrWhiteSpace(neighborhood, "invalid neighborhood"),
                () => Result.FailIfIsNullOrWhiteSpace(city, "invalid city"),
                () => Result.FailIfIsNullOrWhiteSpace(state, "invalid state"),
                () => Result.FailIfIsNullOrWhiteSpace(country, "invalid country"),
                () => Result.FailIfIsNullOrWhiteSpace(zipCode, "invalid zip code"
            )) is { IsFail: true, Error: var err }
        )
        { return err; }

        return new Address(
            street,
            number,
            neighborhood,
            city,
            state,
            country,
            zipCode);
    }
}

