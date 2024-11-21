using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public record DatePeriod
{
    public int Year { get; }
    public int Month { get; }

    private DatePeriod(int year, int month) => (Year, Month) = (year, month);

    private static Result<DatePeriod> Create(int year, int month)
    {
        if (year <= 1900) return new Error("date_period", "invalid year");
        if (month is < 1 or > 12) return new Error("date_period", "invalid month");

        return new DatePeriod(year, month);
    }

    public static implicit operator DatePeriod(DateOnly date) =>
        date.ToDateTime(TimeOnly.MinValue);

    public static implicit operator DatePeriod(DateTime date) =>
        new(date.Year, date.Month);

    public override int GetHashCode() =>
        HashCode.Combine(Year, Month);

    public override string ToString() =>
        $"{Month}/{Year}";

    public static bool operator <(DatePeriod left, DatePeriod right)
    {
        if (left.Year != right.Year)
        {
            return left.Year < right.Year;
        }
        return left.Month < right.Month;
    }

    public static bool operator >(DatePeriod left, DatePeriod right)
    {
        if (left.Year != right.Year)
        {
            return left.Year > right.Year;
        }
        return left.Month > right.Month;
    }

    public static bool operator <=(DatePeriod left, DatePeriod right)
    {
        return left < right || left == right;
    }

    public static bool operator >=(DatePeriod left, DatePeriod right)
    {
        return left > right || left == right;
    }
}
