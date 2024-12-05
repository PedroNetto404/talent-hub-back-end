using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Models.Request;

public sealed record AddressRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Street { get; init; }

    [Required]
    [StringLength(15, MinimumLength = 1)]
    public required string Number { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Neighborhood { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string City { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string State { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Country { get; init; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string ZipCode { get; init; }

    public Address ToValueObject() =>
        Address.Create(
            Street,
            Number,
            Neighborhood,
            City,
            State,
            Country,
            ZipCode
        );
}
