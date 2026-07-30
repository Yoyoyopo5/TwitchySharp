using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.Unit.Helix.Chat;

public class Test_EmoteImageTemplateString
{
    private const string FAKE_TEMPLATE = "https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}";
    private static readonly EmoteImageTemplateString FakeTemplate = new(FAKE_TEMPLATE);

    [Fact]
    public void CreateEmoteImageUrl_ReplacesAllPlaceholders()
    {
        ImageUrl result = FakeTemplate.CreateEmoteImageUrl(
            new EmoteId("emotesv2_test123"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Small);

        Assert.DoesNotContain("{{id}}", result.ToString());
        Assert.DoesNotContain("{{format}}", result.ToString());
        Assert.DoesNotContain("{{theme_mode}}", result.ToString());
        Assert.DoesNotContain("{{scale}}", result.ToString());
    }

    [Fact]
    public void CreateEmoteImageUrl_ExampleTemplate_ProducesCorrectUrl()
    {
        ImageUrl result = FakeTemplate.CreateEmoteImageUrl(
            new EmoteId("emotesv2_abc123"),
            EmoteFormat.Static,
            EmoteTheme.Light,
            EmoteScale.Small);

        Assert.Equal("https://static-cdn.jtvnw.net/emoticons/v2/emotesv2_abc123/static/light/1.0", result);
    }
}
