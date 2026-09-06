namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains a list of VIPs for a specific channel.
/// </summary>
public record GetVipsResponseContent
    : IPageableResponse
{
    /// <summary>
    /// The list of VIPs.
    /// </summary>
    public required ChannelVip[] Data { get; init; }
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
}
