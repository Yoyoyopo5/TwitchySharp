namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains a list of channel information.
/// </summary>
public record GetChannelInformationResponseContent
{
    /// <summary>
    /// A list that contains information about the specified channels. 
    /// The list is empty if the specified channels weren’t found.
    /// </summary>
    public required ChannelInformation[] Data { get; init; }
}
