namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes how an extension is rendered if activated as a video-overlay extension.
/// </summary>
public record VideoOverlayExtensionView
{
    /// <summary>
    /// The HTML file that is shown to viewers on the channel page when the extension is activated on the Video - Overlay slot.
    /// </summary>
    public required string ViewerUrl { get; init; }
    /// <summary>
    /// Determines whether the extension can link to non-Twitch domains.
    /// </summary>
    public required bool CanLinkExternalContent { get; init; }
}
