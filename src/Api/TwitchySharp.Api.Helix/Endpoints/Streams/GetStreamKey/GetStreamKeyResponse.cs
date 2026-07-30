namespace TwitchySharp.Api.Helix.Streams;
/// <inheritdoc cref="ChannelStreamKey"/>
public record GetStreamKeyResponse
{
    /// <summary>
    /// A list containing a single object with the stream key.
    /// </summary>
    public required ChannelStreamKey[] Data { get; init; }
}
