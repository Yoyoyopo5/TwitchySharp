namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains a broadcaster's stream key.
/// </summary>
public record ChannelStreamKey
{
    /// <summary>
    /// The broadcaster's stream key.
    /// </summary>
    public required StreamKey StreamKey { get; init; }
}