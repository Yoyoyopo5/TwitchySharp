using TwitchySharp.Serialization;

namespace TwitchySharp.Core.Tests.Unit.Serialization;

public class Test_DateTimeRfc3339Extension
{
    [Fact]
    public void ToRfc3339_UtcDateTime_EndsWithZ()
    {
        DateTime dateTime = new(2024, 1, 15, 10, 30, 45, DateTimeKind.Utc);

        string result = dateTime.ToRfc3339();

        Assert.EndsWith("Z", result);
    }

    [Fact]
    public void ToRfc3339_UtcDateTime_ProducesCorrectFormat()
    {
        DateTime dateTime = new(2024, 1, 15, 10, 30, 45, DateTimeKind.Utc);

        string result = dateTime.ToRfc3339();

        Assert.Equal("2024-01-15T10:30:45Z", result);
    }

    [Fact]
    public void ToRfc3339_MidnightUtc_HasZeroedTime()
    {
        DateTime dateTime = new(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);

        string result = dateTime.ToRfc3339();

        Assert.Equal("2024-01-15T00:00:00Z", result);
    }

    [Fact]
    public void ToRfc3339_DateOnly_HasZeroedTime()
    {
        DateTime dateTime = new DateTime(2024, 6, 20, 14, 30, 0, DateTimeKind.Utc).Date;

        string result = dateTime.ToRfc3339();

        Assert.Equal("2024-06-20T00:00:00Z", result);
    }

    [Fact]
    public void ToRfc3339_NoFractionalSeconds()
    {
        DateTime dateTime = new(2024, 1, 15, 10, 30, 45, 123, DateTimeKind.Utc);

        string result = dateTime.ToRfc3339();

        Assert.DoesNotContain(".", result);
        Assert.Equal("2024-01-15T10:30:45Z", result);
    }

    [Fact]
    public void ToRfc3339_LocalDateTime_IncludesOffset()
    {
        DateTime dateTime = new(2024, 1, 15, 10, 30, 45, DateTimeKind.Local);

        string result = dateTime.ToRfc3339();

        // Local kind produces offset like +05:00 or -08:00
        // We can't know the exact offset, but it should contain + or - (not Z)
        Assert.False(result.EndsWith('Z'), "Local DateTime should not end with Z");
        Assert.Matches(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}[+-]\d{2}:\d{2}", result);
    }

    [Fact]
    public void ToRfc3339_UnspecifiedKind_NoTimezoneInfo()
    {
        DateTime dateTime = new(2024, 1, 15, 10, 30, 45, DateTimeKind.Unspecified);

        string result = dateTime.ToRfc3339();

        // Unspecified kind produces no timezone suffix
        Assert.Equal("2024-01-15T10:30:45", result);
    }

    [Fact]
    public void ToRfc3339_FromDateTimeOffset_UtcDateTime_ProducesCorrectFormat()
    {
        // This tests the typical usage pattern: DateTimeOffset.UtcDateTime.ToRfc3339()
        DateTimeOffset dateTimeOffset = new DateTimeOffset(2024, 1, 15, 10, 30, 45, TimeSpan.FromHours(-5));

        string result = dateTimeOffset.UtcDateTime.ToRfc3339();

        // UtcDateTime converts to UTC (adds 5 hours) and returns Kind=Utc
        Assert.Equal("2024-01-15T15:30:45Z", result);
    }

    [Fact]
    public void ToRfc3339_FromDateTimeOffset_UtcDateTimeDate_ZeroesTime()
    {
        DateTimeOffset dateTimeOffset = new(2024, 1, 15, 10, 30, 45, TimeSpan.Zero);

        string result = dateTimeOffset.UtcDateTime.Date.ToRfc3339();

        Assert.Equal("2024-01-15T00:00:00Z", result);
    }
}
