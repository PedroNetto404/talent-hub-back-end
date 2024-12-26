using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public sealed record DatePeriod
{
    public int Year { get; }
    public int Month { get; }

    private DatePeriod(int year, int month) => (Year, Month) = (year, month);

    public static Result<DatePeriod> Create(int year, int month)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(year <= 1900, "Year must be greater than 1900."),
                () => Result.FailIf(month is < 1 or > 12, "Month must be between 1 and 12.")) is { IsFail: true } result
        )
        {
            return result.Error;
        }

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

    public static bool operator <=(DatePeriod left, DatePeriod right) => left < right || left == right;

    public static bool operator >=(DatePeriod left, DatePeriod right) => left > right || left == right;
}
