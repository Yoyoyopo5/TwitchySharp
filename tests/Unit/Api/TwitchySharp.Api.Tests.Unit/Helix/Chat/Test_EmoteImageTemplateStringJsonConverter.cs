using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests;

namespace TwitchySharp.Api.Tests.Unit.Helix.Chat;

public class Test_EmoteImageTemplateStringJsonConverter
{
    private readonly EmoteImageTemplateStringJsonConverter _converter = new();

    [Fact]
    public void Read_ValidString_ReturnsEmoteImageTemplateString()
    {
        const string json = "\"https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}\"";

        var result = _converter.Read(json);

        Assert.Equal("https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}", result.TemplateString);
    }

    [Fact]
    public void Read_NonStringToken_ThrowsJsonException()
    {
        const string json = "123";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void Read_NullString_ThrowsJsonException()
    {
        const string json = "null";

        var (isSuccessful, _) = _converter.TryRead(json);

        Assert.False(isSuccessful);
    }

    [Fact]
    public void Write_EmoteImageTemplateString_WritesTemplateString()
    {
        var template = new EmoteImageTemplateString
        {
            TemplateString = "https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}"
        };

        var result = _converter.Write(template);

        Assert.Equal("\"https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}\"", result);
    }

    [Fact]
    public void RoundTrip_PreservesValue()
    {
        var original = new EmoteImageTemplateString
        {
            TemplateString = "https://example.com/{{id}}/{{format}}"
        };

        var json = _converter.Write(original);
        var result = _converter.Read(json);

        Assert.Equal(original.TemplateString, result.TemplateString);
    }
}
