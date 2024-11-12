using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public record DatePeriod : IComparable<DatePeriod>
{
    public int Year { get; }
    public int Month { get; }

    private DatePeriod(int year, int month) => (Year, Month) = (year, month);

    private static Result<DatePeriod> Create(int year, int month) 
    {
        if(year <= 1900) return Error.Displayable("date_period", "invalid year");
        if(month is < 1 or > 12) return Error.Displayable("date_period", "invalid month");

        return new DatePeriod(year, month);
    }

    public static implicit operator DatePeriod(DateOnly date) =>
        date.ToDateTime(TimeOnly.MinValue);

    public static implicit operator DatePeriod(DateTime date) =>
        new(date.Year, date.Month);

    public int CompareTo(DatePeriod other) =>
        other.Year != Year
        ? other.Year.CompareTo(Year)
        : other.Month.CompareTo(Month);

    public static bool operator ==(DatePeriod left, DatePeriod right) =>
           left.Year == right.Year && left.Month == right.Month;

    public static bool operator !=(DatePeriod left, DatePeriod right) =>
        !(left == right);

    public static bool operator <(DatePeriod left, DatePeriod right) =>
        left.CompareTo(right) < 0;

    public static bool operator <=(DatePeriod left, DatePeriod right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >(DatePeriod left, DatePeriod right) =>
        left.CompareTo(right) > 0;

    public static bool operator >=(DatePeriod left, DatePeriod right) =>
        left.CompareTo(right) >= 0;

    public override bool Equals(object? obj) =>
        obj is DatePeriod period && this == period;

    public override int GetHashCode() =>
        HashCode.Combine(Year, Month);

    public override string ToString() =>
        $"{Month}/{Year}";
}
