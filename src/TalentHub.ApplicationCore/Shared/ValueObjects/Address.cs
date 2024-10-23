namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public record Address(
    string Street,
    string Number,
    string Neighborhood,
    string State,
    string Country,
    string ZipCode
);
