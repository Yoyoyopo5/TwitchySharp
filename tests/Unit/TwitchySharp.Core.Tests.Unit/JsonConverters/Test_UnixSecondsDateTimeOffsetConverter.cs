using TwitchySharp.Serialization;
using TwitchySharp.Tests;

namespace TwitchySharp.Core.Tests.Unit.JsonConverters;

public class Test_UnixSecondsDateTimeOffsetConverter
{
    private readonly UnixSecondsDateTimeOffsetConverter _converter = new();

    [Fact]
    public void Read_NumberToken_ReturnsDateTimeOffset()
    {
        const string json = "1705312200";
        var expected = DateTimeOffset.FromUnixTimeSeconds(1705312200);

        var result = _converter.Read(json);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Read_StringToken_ReturnsDateTimeOffset()
    {
        const string json = "\"1705312200\"";
        var expected = DateTimeOffset.FromUnixTimeSeconds(1705312200);

        var result = _converter.Read(json);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Read_InvalidToken_ThrowsException()
    {
        const string json = "true";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void Write_DateTimeOffset_WritesUnixSeconds()
    {
        var value = DateTimeOffset.FromUnixTimeSeconds(1705312200);

        var result = _converter.Write(value);

        Assert.Equal("1705312200", result);
    }

    [Fact]
    public void RoundTrip_PreservesValue()
    {
        var original = DateTimeOffset.FromUnixTimeSeconds(1705312200);

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.Equal(original, result);
    }

    [Fact]
    public void Read_Zero_ReturnsUnixEpoch()
    {
        const string json = "0";

        var result = _converter.Read(json);

        Assert.Equal(DateTimeOffset.UnixEpoch, result);
    }
}
