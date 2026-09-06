namespace TwitchySharp.Api.Helix.Clips;

/// <summary>
/// Contains an array of download information for a set of Twitch clips.
/// </summary>
public record GetClipsDownloadResponseContent
{
    /// <summary>
    /// An array of clips download information.
    /// </summary>
    public required TwitchClipDownload[] Data { get; init; }
}
