namespace TwitchySharp.Core.Tests.Unit.Primitives;

public class Test_ImageUrlTemplate
{
    [Theory]
    [InlineData("https://example.com/{width}x{height}.jpg", 320, 180)]
    [InlineData("https://static-cdn.jtvnw.net/ttv-boxart/33214-{width}x{height}.jpg", 0, 0)]
    [InlineData("https://example.com/{width}/{height}/{width}x{height}.jpg", 160, 160)]
    [InlineData("https://example.com/image.jpg", 160, 160)]
    public void ToImageUrl_ReplacesWidthAndHeight(string templateUrl, uint width, uint height)
    {
        ImageUrlTemplate template = new(templateUrl);
        string expected = templateUrl.Replace("{width}", width.ToString()).Replace("{height}", height.ToString());

        Uri result = template.ToImageUrl(width, height);

        Assert.Equal(expected, result.ToString());
    }
}
