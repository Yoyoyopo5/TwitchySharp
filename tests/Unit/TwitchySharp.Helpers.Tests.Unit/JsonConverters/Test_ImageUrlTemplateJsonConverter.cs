using TwitchySharp.Tests;

namespace TwitchySharp.Helpers.Tests.Unit.JsonConverters;

public class Test_ImageUrlTemplateJsonConverter
{
    private readonly ImageUrlTemplateJsonConverter _converter = new();

    [Fact]
    public void Read_ValidString_ReturnsImageUrlTemplate()
    {
        const string json = "\"https://static-cdn.jtvnw.net/ttv-boxart/33214-{width}x{height}.jpg\"";

        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Equal("https://static-cdn.jtvnw.net/ttv-boxart/33214-{width}x{height}.jpg", result.TemplateUrl);
    }

    [Fact]
    public void Read_Null_ReturnsNull()
    {
        const string json = "null";

        var result = _converter.Read(json);

        Assert.Null(result);
    }

    [Fact]
    public void Write_ImageUrlTemplate_WritesTemplateUrl()
    {
        var template = new ImageUrlTemplate("https://example.com/{width}x{height}.jpg");

        var result = _converter.Write(template);

        Assert.Equal("\"https://example.com/{width}x{height}.jpg\"", result);
    }

    [Fact]
    public void RoundTrip_PreservesValue()
    {
        var original = new ImageUrlTemplate("https://static-cdn.jtvnw.net/ttv-boxart/33214-{width}x{height}.jpg");

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Equal(original.TemplateUrl, result.TemplateUrl);
    }
}
