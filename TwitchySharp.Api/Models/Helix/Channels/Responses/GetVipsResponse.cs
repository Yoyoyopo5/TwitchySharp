using TwitchySharp.Api.Models.Helix.Channels.Models;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Models.Helix.Channels.Responses;
/// <summary>
/// Contains a list of VIPs for a specific channel.
/// </summary>
public record GetVipsResponse
{
    /// <summary>
    /// The list of VIPs.
    /// </summary>
    public required ChannelVip[] Data { get; init; }
    /// <inheritdoc cref="Shared.Pagination"/>
    public required Pagination Pagination { get; init; }
}
