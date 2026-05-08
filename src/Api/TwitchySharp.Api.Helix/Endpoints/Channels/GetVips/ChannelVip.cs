
namespace TwitchySharp.Api.Helix.Channels;

/// <summary>
/// Contains information about a specific VIP on a channel.
/// </summary>
public record ChannelVip
{
    /// <summary>
    /// The user id of the VIP.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the VIP.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the VIP.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
}
