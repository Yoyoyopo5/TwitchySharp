using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Tests;

namespace TwitchySharp.Api.Tests.Unit.Helix.Videos;

public class Test_VideoThumbnailUrlJsonConverter
{
    private readonly VideoThumbnailUrlJsonConverter _converter = new();

    [Fact]
    public void Read_ValidString_ReturnsVideoThumbnailUrl()
    {
        const string json = "\"https://static-cdn.jtvnw.net/cf_vods/d2nvs31859zcd8/twitchdev/335921245/thumb/index-0000000000-%{width}x%{height}.jpg\"";

        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Contains("%{width}", result.Value);
        Assert.Contains("%{height}", result.Value);
    }

    [Fact]
    public void Read_Null_ReturnsNull()
    {
        const string json = "null";

        var result = _converter.Read(json);

        Assert.Null(result);
    }

    [Fact]
    public void Write_VideoThumbnailUrl_WritesTemplateUrl()
    {
        var thumbnail = new VideoThumbnailUrl("https://example.com/thumb-%{width}x%{height}.jpg");

        var result = _converter.Write(thumbnail);

        Assert.Equal("\"https://example.com/thumb-%{width}x%{height}.jpg\"", result);
    }

    [Fact]
    public void RoundTrip_PreservesValue()
    {
        var original = new VideoThumbnailUrl("https://example.com/thumb-%{width}x%{height}.jpg");

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.NotNull(result);
        Assert.Equal(original.Value, result.Value);
    }

    [Fact]
    public void VideoThumbnailUrl_ToImageUrl_ReplacesPlaceholders()
    {
        var thumbnail = new VideoThumbnailUrl("https://example.com/thumb-%{width}x%{height}.jpg");

        var result = thumbnail.ToImageUrl(320, 180);

        Assert.Equal("https://example.com/thumb-320x180.jpg", result.ToString());
    }

    [Fact]
    public void VideoThumbnailUrl_ToImageUrl_ExampleTemplate_ProducesCorrectUrl()
    {
        var thumbnail = new VideoThumbnailUrl("https://static-cdn.jtvnw.net/cf_vods/d2nvs31859zcd8/twitchdev/335921245/thumb/index-0000000000-%{width}x%{height}.jpg");

        var result = thumbnail.ToImageUrl(320, 180);

        Assert.Equal("https://static-cdn.jtvnw.net/cf_vods/d2nvs31859zcd8/twitchdev/335921245/thumb/index-0000000000-320x180.jpg", result.ToString());
    }
}
