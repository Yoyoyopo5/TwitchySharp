namespace TwitchySharp.Helpers.Tests.Unit;

public class Test_ImageUrlTemplate
{
    [Fact]
    public void ToImageUrl_ReplacesWidthAndHeight()
    {
        var template = new ImageUrlTemplate("https://example.com/{width}x{height}.jpg");

        var result = template.ToImageUrl(320, 180);

        Assert.Equal("https://example.com/320x180.jpg", result.ToString());
    }

    [Fact]
    public void ToImageUrl_ReturnsValidUri()
    {
        var template = new ImageUrlTemplate("https://example.com/{width}x{height}.jpg");

        var result = template.ToImageUrl(640, 360);

        Assert.True(result.IsAbsoluteUri);
        Assert.Equal("https", result.Scheme);
    }

    [Fact]
    public void ToImageUrl_ExampleTemplate_ProducesCorrectUrl()
    {
        const string templateUrl = "https://static-cdn.jtvnw.net/ttv-boxart/33214-{width}x{height}.jpg";
        var template = new ImageUrlTemplate(templateUrl);

        var result = template.ToImageUrl(320, 180);

        Assert.Equal("https://static-cdn.jtvnw.net/ttv-boxart/33214-320x180.jpg", result.ToString());
    }

    [Fact]
    public void ToImageUrl_LargeSize_ProducesCorrectUrl()
    {
        var template = new ImageUrlTemplate("https://example.com/{width}x{height}.jpg");

        var result = template.ToImageUrl(1920, 1080);

        Assert.Equal("https://example.com/1920x1080.jpg", result.ToString());
    }

    [Fact]
    public void ToImageUrl_ZeroSize_ProducesCorrectUrl()
    {
        var template = new ImageUrlTemplate("https://example.com/{width}x{height}.jpg");

        var result = template.ToImageUrl(0, 0);

        Assert.Equal("https://example.com/0x0.jpg", result.ToString());
    }

    [Fact]
    public void ToImageUrl_MultipleReplacements_ReplacesAll()
    {
        var template = new ImageUrlTemplate("https://example.com/{width}/{height}/{width}x{height}.jpg");

        var result = template.ToImageUrl(100, 200);

        Assert.Equal("https://example.com/100/200/100x200.jpg", result.ToString());
    }
}
