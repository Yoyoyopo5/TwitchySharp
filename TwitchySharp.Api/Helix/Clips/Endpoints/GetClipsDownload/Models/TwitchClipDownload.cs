namespace TwitchySharp.Api.Helix.Clips;

/// <summary>
/// Contains information about Twitch clip downloads.
/// </summary>
public record TwitchClipDownload
{
    /// <summary>
    /// The id of the clip.
    /// </summary>
    public required string ClipId { get; init; }
    /// <summary>
    /// A URL pointing to a downloadable landscape format video of the clip.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the download is unavailable.
    /// </remarks>
    public string? LandscapeDownloadUrl { get; init; }
    /// <summary>
    /// A URL pointing to a downloadable portrait format video of the clip.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the download is unavailable.
    /// </remarks>
    public string? PortraitDownloadUrl { get; init; }
}
