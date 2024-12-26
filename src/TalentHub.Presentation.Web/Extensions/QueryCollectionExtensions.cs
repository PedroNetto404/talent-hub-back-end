using Humanizer;

namespace TalentHub.Presentation.Web.Extensions;

public static class QueryCollectionExtensions
{
    private static readonly Dictionary<Type, Func<string, object>> Converters = new()
    {
        { typeof(Guid), value => Guid.Parse(value) },
        { typeof(int), value => int.Parse(value) },
        { typeof(long), value => long.Parse(value) },
        { typeof(string), value => value },
        { typeof(bool), value => bool.Parse(value) },
        { typeof(DateTime), value => DateTime.Parse(value) },
        { typeof(DateTimeOffset), value => DateTimeOffset.Parse(value) },
        { typeof(decimal), value => decimal.Parse(value) },
        { typeof(float), value => float.Parse(value) },
        { typeof(double), value => double.Parse(value) },
        { typeof(IEnumerable<Guid>), value => value.Split(',').Select(Guid.Parse).ToList() },
        { typeof(IEnumerable<int>), value => value.Split(',').Select(int.Parse).ToList() },
        { typeof(IEnumerable<long>), value => value.Split(',').Select(long.Parse).ToList() },
        { typeof(IEnumerable<string>), value => value.Split(',').ToList() },
        { typeof(IEnumerable<bool>), value => value.Split(',').Select(bool.Parse).ToList() },
        { typeof(IEnumerable<DateTime>), value => value.Split(',').Select(DateTime.Parse).ToList() },
        { typeof(IEnumerable<DateTimeOffset>), value => value.Split(',').Select(DateTimeOffset.Parse).ToList() },
        { typeof(IEnumerable<decimal>), value => value.Split(',').Select(decimal.Parse).ToList() },
        { typeof(IEnumerable<float>), value => value.Split(',').Select(float.Parse).ToList() },
        { typeof(IEnumerable<double>), value => value.Split(',').Select(double.Parse).ToList() }
    };

    public static T Get<T>(this IQueryCollection query, string key, T defaultValue)
    {
        if (!Converters.TryGetValue(typeof(T), out Func<string, object>? converter))
        {
            throw new NotSupportedException($"Type {typeof(T).Name} is not supported.");
        }

        string? value = query[key].FirstOrDefault();
        if(value == null)
        {
            return defaultValue;
        }

        return (T)converter(value);
    }

    public static T GetEnum<T>(this IQueryCollection query, string key, T defaultValue) where T : struct
    {
        string? value = query[key].FirstOrDefault();
        if (value == null)
        {
            return defaultValue;
        }

        return Enum.TryParse(value.Pascalize(), true, out T result) ? result : defaultValue;
    }
}
