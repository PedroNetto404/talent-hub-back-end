using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.Presentation.Web.Shared.RequestModels;

public record PageRequest
{
    [FromQuery(Name = "_limit")]
    [DefaultValue(10)]
    [Range(1, int.MaxValue)]
    public int? Limit { get; init; } = 10;

    [FromQuery(Name = "_offset")]
    [DefaultValue(0)]
    [Range(0, int.MaxValue)]
    public int? Offset { get; init; } = 0;

    [FromQuery(Name = "_sort_by")]
    [DefaultValue("id")]
    public string? SortBy { get; init; } = "id";

    [FromQuery(Name = "_sort_order")]
    [DefaultValue(true)]
    public SortOrder? SortOrder { get; init; } = ApplicationCore.Shared.Enums.SortOrder.Ascending;
}
