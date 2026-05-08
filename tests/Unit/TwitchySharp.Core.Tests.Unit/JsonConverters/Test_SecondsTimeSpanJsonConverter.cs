using TwitchySharp.Serialization;
using TwitchySharp.Tests;

namespace TwitchySharp.Core.Tests.Unit.JsonConverters;

public class Test_SecondsTimeSpanJsonConverter
{
    private readonly SecondsTimeSpanJsonConverter _converter = new();

    [Fact]
    public void Read_NumberValue_ReturnsTimeSpanFromSeconds()
    {
        const string json = "60";
        var expected = TimeSpan.FromSeconds(60);

        var actual = _converter.Read(json);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Read_StringValue_ReturnsTimeSpanFromSeconds()
    {
        const string json = "\"90\"";
        var expected = TimeSpan.FromSeconds(90);

        var actual = _converter.Read(json);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Read_DecimalValue_ReturnsTimeSpanWithFractionalSeconds()
    {
        const string json = "30.5";
        var expected = TimeSpan.FromSeconds(30.5);

        var actual = _converter.Read(json);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Read_ZeroValue_ReturnsZeroTimeSpan()
    {
        const string json = "0";
        var expected = TimeSpan.Zero;

        var actual = _converter.Read(json);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Write_TimeSpan_WritesSecondValue()
    {
        var timeSpan = TimeSpan.FromSeconds(120);
        const string expected = "120";

        var actual = _converter.Write(timeSpan);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Write_TimeSpanWithMinutes_WritesSecondsValue()
    {
        var timeSpan = TimeSpan.FromMinutes(2);
        const string expected = "120";

        var actual = _converter.Write(timeSpan);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Write_ZeroTimeSpan_WritesZero()
    {
        var timeSpan = TimeSpan.Zero;
        const string expected = "0";

        var actual = _converter.Write(timeSpan);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Read_InvalidToken_ThrowsJsonException()
    {
        const string json = "true";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void RoundTrip_TimeSpan_PreservesValue()
    {
        var original = TimeSpan.FromSeconds(45.5);

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.Equal(original, result);
    }
}
