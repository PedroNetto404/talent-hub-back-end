using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Models.Request;

public record PagedRequest
{
    [FromQuery(Name = "_limit")]
    [DefaultValue(10)]
    [Range(1, int.MaxValue)]
    public int Limit { get; init; } = 10;

    [FromQuery(Name = "_offset")]
    [DefaultValue(0)]
    [Range(0, int.MaxValue)]
    public int Offset { get; init; } = 0;

    [FromQuery(Name = "_sort_by")]
    [DefaultValue("id")]
    public string SortBy { get; init; } = "id";

    [FromQuery(Name = "_sort_ascending")]
    [DefaultValue(true)]
    public bool Ascending { get; init; } = true;
}