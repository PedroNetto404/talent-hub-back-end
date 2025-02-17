namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public sealed record Address(
    string Street,
    string Number,
    string Neighborhood,
    string City,
    string State,
    string Country,
    string ZipCode
);
