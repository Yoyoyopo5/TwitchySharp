namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains a list of editors on a specific channel.
/// </summary>
public record GetChannelEditorsResponse
{
    /// <summary>
    /// A list of users that are editors for the specified broadcaster. 
    /// The list is empty if the broadcaster doesn’t have editors.
    /// </summary>
    public required ChannelEditor[] Data { get; init; }
}
