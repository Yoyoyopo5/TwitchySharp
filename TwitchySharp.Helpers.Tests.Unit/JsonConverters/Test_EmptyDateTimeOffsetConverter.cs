using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Tests;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;

public class Test_EmptyDateTimeOffsetConverter
{
    private readonly EmptyDateTimeOffsetConverter _converter = new();

    [Fact]
    public void Read_ValidDateTimeString_ReturnsDateTimeOffset()
    {
        const string json = "\"2024-01-15T10:30:00Z\"";

        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Equal(new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero), result.Value);
    }

    [Fact]
    public void Read_EmptyString_ReturnsNull()
    {
        const string json = "\"\"";

        var result = _converter.Read(json);

        Assert.Null(result);
    }

    [Fact]
    public void Read_InvalidDateString_ReturnsNull()
    {
        const string json = "\"not-a-date\"";

        var result = _converter.Read(json);

        Assert.Null(result);
    }

    [Fact]
    public void Write_DateTimeOffset_WritesString()
    {
        var value = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);

        var result = _converter.Write(value);

        // The output format is locale-dependent, so just verify it contains the year
        Assert.Contains("2024", result);
        Assert.StartsWith("\"", result);
        Assert.EndsWith("\"", result);
    }

    [Fact]
    public void Write_Null_WritesEmptyString()
    {
        DateTimeOffset? value = null;

        var result = _converter.Write(value);

        Assert.Equal("\"\"", result);
    }

    [Fact]
    public void RoundTrip_ValidDateTime_PreservesValue()
    {
        var original = new DateTimeOffset(2024, 6, 15, 14, 30, 0, TimeSpan.Zero);

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Equal(original, result.Value);
    }
}
