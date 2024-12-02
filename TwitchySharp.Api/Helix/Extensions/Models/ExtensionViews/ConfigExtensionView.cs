namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes the view that is shown to broadcasters while they are configuring an extension within the Extension Manager.
/// </summary>
public record ConfigExtensionView
{
    /// <summary>
    /// The HTML file shown to broadcasters while they are configuring the extension within the Extension Manager.
    /// </summary>
    public required string ViewerUrl { get; init; }
    /// <summary>
    /// Determines whether the extension can link to non-Twitch domains.
    /// </summary>
    public required bool CanLinkExternalContent { get; init; }
}
