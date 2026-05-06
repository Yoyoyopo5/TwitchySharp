using System;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// Contains image urls for channel point reward images.
/// </summary>
public record RewardImage
{
    /// <summary>
    /// The URL to a small version of the image.
    /// </summary>
    [JsonPropertyName("url_1x")]
    public required Uri Url1x { get; init; }
    /// <summary>
    /// The URL to a medium version of the image.
    /// </summary>
    [JsonPropertyName("url_2x")]
    public required Uri Url2x { get; init; }
    /// <summary>
    /// The URL to a large version of the image.
    /// </summary>
    [JsonPropertyName("url_4x")]
    public required Uri Url4x { get; init; }
}
