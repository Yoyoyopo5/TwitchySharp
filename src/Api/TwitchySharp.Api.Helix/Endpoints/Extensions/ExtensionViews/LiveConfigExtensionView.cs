namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Describes the view that is shown to broadcasters in the Live module of the dashboard.
/// </summary>
public record LiveConfigExtensionView
{
    /// <summary>
    /// The HTML file that is shown to broadcasters in the Live module of the dashboard.
    /// It is used for actions that broadcasters take while the extension is active, such as creating a new poll.
    /// </summary>
    public required Uri ViewerUrl { get; init; }
    /// <summary>
    /// Determines whether the extension can link to non-Twitch domains.
    /// </summary>
    public required bool CanLinkExternalContent { get; init; }
}
