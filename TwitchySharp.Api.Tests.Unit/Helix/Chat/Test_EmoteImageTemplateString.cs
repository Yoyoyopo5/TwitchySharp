using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.Chat;

public class Test_EmoteImageTemplateString
{
    private const string ExampleTemplate = "https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}";

    [Fact]
    public void CreateEmoteImageUrl_ReplacesAllPlaceholders()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("emotesv2_test123"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Small);

        Assert.DoesNotContain("{{id}}", result);
        Assert.DoesNotContain("{{format}}", result);
        Assert.DoesNotContain("{{theme_mode}}", result);
        Assert.DoesNotContain("{{scale}}", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_ExampleTemplate_ProducesCorrectUrl()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("emotesv2_abc123"),
            EmoteFormat.Static,
            EmoteTheme.Light,
            EmoteScale.Small);

        Assert.Equal("https://static-cdn.jtvnw.net/emoticons/v2/emotesv2_abc123/static/light/1.0", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithStaticFormat_IncludesStatic()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Small);

        Assert.Contains("/static/", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithAnimatedFormat_IncludesAnimated()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Animated,
            EmoteTheme.Dark,
            EmoteScale.Small);

        Assert.Contains("/animated/", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithLightTheme_IncludesLight()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Static,
            EmoteTheme.Light,
            EmoteScale.Small);

        Assert.Contains("/light/", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithDarkTheme_IncludesDark()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Small);

        Assert.Contains("/dark/", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithSmallScale_Includes1x()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Small);

        Assert.EndsWith("/1.0", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithMediumScale_Includes2x()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Medium);

        Assert.EndsWith("/2.0", result);
    }

    [Fact]
    public void CreateEmoteImageUrl_WithLargeScale_Includes3x()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.CreateEmoteImageUrl(
            new EmoteId("test"),
            EmoteFormat.Static,
            EmoteTheme.Dark,
            EmoteScale.Large);

        Assert.EndsWith("/3.0", result);
    }

    [Fact]
    public void ToString_ReturnsTemplateString()
    {
        var template = new EmoteImageTemplateString { TemplateString = ExampleTemplate };

        var result = template.ToString();

        Assert.Equal(ExampleTemplate, result);
    }
}
