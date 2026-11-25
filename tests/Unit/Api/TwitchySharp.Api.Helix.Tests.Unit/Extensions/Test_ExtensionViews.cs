using System.Text.Json;
using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Tests.Unit.Helix.Extensions;

public class Test_ExtensionViews
{
    private static readonly JsonSerializerOptions JsonOptions = JsonConfig.ApiOptions;

    [Fact]
    public void Deserialize_WithLiveConfig_ParsesLiveConfig()
    {
        const string viewsJson = """
        {
          "config": {
            "viewer_url": "https://example.com/config.html",
            "can_link_external_content": false
          },
          "live_config": {
            "viewer_url": "https://example.com/live_config.html",
            "can_link_external_content": true
          }
        }
        """;

        ExtensionViews? views = JsonSerializer.Deserialize<ExtensionViews>(viewsJson, JsonOptions);

        Assert.NotNull(views);
        Assert.NotNull(views.LiveConfig);
        Assert.Equal(new Uri("https://example.com/live_config.html"), views.LiveConfig.ViewerUrl);
        Assert.True(views.LiveConfig.CanLinkExternalContent);
        Assert.Equal(new Uri("https://example.com/config.html"), views.Config.ViewerUrl);
        Assert.False(views.Config.CanLinkExternalContent);
    }

    [Fact]
    public void Deserialize_WithoutLiveConfig_LiveConfigIsNull()
    {
        const string viewsJson = """
        {
          "config": {
            "viewer_url": "https://example.com/config.html",
            "can_link_external_content": false
          }
        }
        """;

        ExtensionViews? views = JsonSerializer.Deserialize<ExtensionViews>(viewsJson, JsonOptions);

        Assert.NotNull(views);
        Assert.Null(views.LiveConfig);
    }
}
