namespace TalentHub.ApplicationCore.Shared.ValueObjects;

public readonly struct DatePeriod : IComparable<DatePeriod>
{
    public int Year { get; }
    public int Month { get; }

    public DatePeriod(int year, int month)
    {
        if (year is < 0)
        {
            throw new ArgumentException("Out of range", nameof(year));
        }

        if (month is < 1 or > 12)
        {
            throw new ArgumentException("Month out of range", nameof(month));
        }

        Year = year;
        Month = month;
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
