namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Contains information related to a newly captured clip.
/// </summary>
public record CreateClipResponseContent
{
    /// <summary>
    /// A URL that you can use to edit the clip’s title, identify the part of the clip to publish, and publish the clip.
    /// The URL is valid for up to 24 hours or until the clip is published, whichever comes first.
    /// <see href="https://help.twitch.tv/s/article/how-to-use-clips">Learn More</see>.
    /// </summary>
    public required Uri EditUrl { get; init; }
    /// <summary>
    /// An id that uniquely identifies the clip.
    /// </summary>
    public required ClipId Id { get; init; }
}
