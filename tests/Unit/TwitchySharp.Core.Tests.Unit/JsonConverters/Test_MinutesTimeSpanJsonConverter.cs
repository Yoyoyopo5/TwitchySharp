using TwitchySharp.Serialization;
using TwitchySharp.Tests;

namespace TwitchySharp.Core.Tests.Unit.JsonConverters;

public class Test_MinutesTimeSpanJsonConverter
{
    private readonly MinutesTimeSpanJsonConverter _converter = new();

    [Fact]
    public void Read_NumberValue_ReturnsTimeSpanFromMinutes()
    {
        const string json = "30";
        var expected = TimeSpan.FromMinutes(30);

        var actual = _converter.Read(json);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Read_StringValue_ReturnsTimeSpanFromMinutes()
    {
        const string json = "\"45\"";
        var expected = TimeSpan.FromMinutes(45);

        var actual = _converter.Read(json);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Read_DecimalValue_ReturnsTimeSpanWithFractionalMinutes()
    {
        const string json = "1.5";
        var expected = TimeSpan.FromMinutes(1.5);

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
    public void Write_TimeSpan_WritesMinutesValue()
    {
        var timeSpan = TimeSpan.FromMinutes(60);
        const string expected = "60";

        var actual = _converter.Write(timeSpan);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Write_TimeSpanWithHours_WritesMinutesValue()
    {
        var timeSpan = TimeSpan.FromHours(2);
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
        var original = TimeSpan.FromMinutes(90);

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.Equal(original, result);
    }
}
