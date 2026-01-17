using TwitchySharp.Api.Models.Helix.Channels.Models;

namespace TwitchySharp.Api.Models.Helix.Channels.Responses;
/// <summary>
/// Contains a list of channel information.
/// </summary>
public record GetChannelInformationResponse
{
    /// <summary>
    /// A list that contains information about the specified channels. 
    /// The list is empty if the specified channels weren’t found.
    /// </summary>
    public required ChannelInformation[] Data { get; init; }
}
