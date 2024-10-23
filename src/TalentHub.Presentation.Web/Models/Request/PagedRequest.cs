using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Models.Request;

public record PagedRequest
{
    [FromQuery(Name = "_limit")]
    [DefaultValue(10)]
    [Range(1, int.MaxValue)]
    public int Limit { get; init; }

    [FromQuery(Name = "_offset")]
    [DefaultValue(0)]
    [Range(0, int.MaxValue)]
    public int Offset { get; init; }

    [FromQuery(Name = "_sort_by")]
    public string? SortBy { get; init; }

    [FromQuery(Name = "_sort_ascending")]
    public bool? SortAscending { get; init; }
}