namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes how an extension is rendered if activated as a panel extension.
/// </summary>
public record PanelExtensionView
{
    /// <summary>
    /// The HTML file that is shown to viewers on the channel page when the extension is activated in a Panel slot.
    /// </summary>
    public required string ViewerUrl { get; init; }
    /// <summary>
    /// Determines whether the extension can link to non-Twitch domains.
    /// </summary>
    public required bool CanLinkExternalContent { get; init; }
    /// <summary>
    /// The height, in pixels, of the panel component that the extension is rendered in.
    /// </summary>
    public required int Height { get; init; }
}
