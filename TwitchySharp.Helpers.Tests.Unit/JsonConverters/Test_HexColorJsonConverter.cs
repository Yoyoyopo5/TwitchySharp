using TwitchySharp.Tests;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;

public class Test_HexColorJsonConverter
{
    private readonly HexColorJsonConverter _converter = new();

    [Fact]
    public void Read_HexStringWithHash_ReturnsRgbColor()
    {
        const string json = "\"#FF5733\"";

        var result = _converter.Read(json);

        Assert.Equal(255, result.R);
        Assert.Equal(87, result.G);
        Assert.Equal(51, result.B);
    }

    [Fact]
    public void Read_HexStringWithoutHash_ReturnsRgbColor()
    {
        const string json = "\"FF5733\"";

        var result = _converter.Read(json);

        Assert.Equal(255, result.R);
        Assert.Equal(87, result.G);
        Assert.Equal(51, result.B);
    }

    [Fact]
    public void Read_InvalidToken_ThrowsException()
    {
        const string json = "123";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void Write_RgbColor_WritesHexString()
    {
        var color = new RgbColor(255, 87, 51);

        var result = _converter.Write(color);

        Assert.Equal("\"#FF5733\"", result);
    }

    [Fact]
    public void RoundTrip_PreservesValue()
    {
        var original = new RgbColor(100, 150, 200);

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.Equal(original.R, result.R);
        Assert.Equal(original.G, result.G);
        Assert.Equal(original.B, result.B);
    }
}
