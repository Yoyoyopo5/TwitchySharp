namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Contains an array containing the created clip.
/// </summary>
public record CreateClipFromVodResponse
{
    /// <summary>
    /// An array containing the single clip that was created.
    /// </summary>
    public required CreatedVodClip[] Data { get; init; }
}