using System.Text.Json;
using Humanizer;

namespace TalentHub.Infra.Json.Converters;

public sealed class HumanizerSnakeCaseJsonPolicy : JsonNamingPolicy
{
    private HumanizerSnakeCaseJsonPolicy() { }
    public static HumanizerSnakeCaseJsonPolicy Instance { get; } = new HumanizerSnakeCaseJsonPolicy();
    public override string ConvertName(string name) => name.Underscore();
}
