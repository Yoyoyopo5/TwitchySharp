namespace TwitchySharp.Helpers.Tests.Unit;

public class Test_DateTimeRfc3339Extension
{
    [Fact]
    public void ToRfc3339_UtcDateTime_EndsWithZ()
    {
        var dateTime = new DateTime(2024, 1, 15, 10, 30, 45, DateTimeKind.Utc);

        var result = dateTime.ToRfc3339();

        Assert.EndsWith("Z", result);
    }

    [Fact]
    public void ToRfc3339_UtcDateTime_ProducesCorrectFormat()
    {
        var dateTime = new DateTime(2024, 1, 15, 10, 30, 45, DateTimeKind.Utc);

        var result = dateTime.ToRfc3339();

        Assert.Equal("2024-01-15T10:30:45Z", result);
    }

    [Fact]
    public void ToRfc3339_MidnightUtc_HasZeroedTime()
    {
        var dateTime = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);

        var result = dateTime.ToRfc3339();

        Assert.Equal("2024-01-15T00:00:00Z", result);
    }

    [Fact]
    public void ToRfc3339_DateOnly_HasZeroedTime()
    {
        var dateTime = new DateTime(2024, 6, 20, 14, 30, 0, DateTimeKind.Utc).Date;

        var result = dateTime.ToRfc3339();

        // .Date preserves the Kind, so UTC DateTime.Date still has Z suffix
        Assert.Equal("2024-06-20T00:00:00Z", result);
    }

    [Fact]
    public void ToRfc3339_NoFractionalSeconds()
    {
        var dateTime = new DateTime(2024, 1, 15, 10, 30, 45, 123, DateTimeKind.Utc);

        var result = dateTime.ToRfc3339();

        Assert.DoesNotContain(".", result);
        Assert.Equal("2024-01-15T10:30:45Z", result);
    }

    [Fact]
    public void ToRfc3339_LocalDateTime_IncludesOffset()
    {
        var dateTime = new DateTime(2024, 1, 15, 10, 30, 45, DateTimeKind.Local);

        var result = dateTime.ToRfc3339();

        // Local kind produces offset like +05:00 or -08:00
        // We can't know the exact offset, but it should contain + or - (not Z)
        Assert.False(result.EndsWith("Z"), "Local DateTime should not end with Z");
        Assert.Matches(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}[+-]\d{2}:\d{2}", result);
    }

    [Fact]
    public void ToRfc3339_UnspecifiedKind_NoTimezoneInfo()
    {
        var dateTime = new DateTime(2024, 1, 15, 10, 30, 45, DateTimeKind.Unspecified);

        var result = dateTime.ToRfc3339();

        // Unspecified kind produces no timezone suffix
        Assert.Equal("2024-01-15T10:30:45", result);
    }

    [Fact]
    public void ToRfc3339_FromDateTimeOffset_UtcDateTime_ProducesCorrectFormat()
    {
        // This tests the typical usage pattern: DateTimeOffset.UtcDateTime.ToRfc3339()
        var dateTimeOffset = new DateTimeOffset(2024, 1, 15, 10, 30, 45, TimeSpan.FromHours(-5));

        var result = dateTimeOffset.UtcDateTime.ToRfc3339();

        // UtcDateTime converts to UTC (adds 5 hours) and returns Kind=Utc
        Assert.Equal("2024-01-15T15:30:45Z", result);
    }

    [Fact]
    public void ToRfc3339_FromDateTimeOffset_UtcDateTimeDate_ZeroesTime()
    {
        // This tests the analytics pattern: DateTimeOffset.UtcDateTime.Date.ToRfc3339()
        var dateTimeOffset = new DateTimeOffset(2024, 1, 15, 10, 30, 45, TimeSpan.Zero);

        var result = dateTimeOffset.UtcDateTime.Date.ToRfc3339();

        // .Date zeros time and preserves UTC Kind, so Z suffix is included
        Assert.Equal("2024-01-15T00:00:00Z", result);
    }
}
