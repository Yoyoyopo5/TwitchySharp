using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Enums.Events.Channel;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Vip"/> action.
/// </summary>
public record ChannelModerateVipAction : IHaveUser
{
    /// <summary>
    /// The id of the user gaining VIP status.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user gaining VIP status.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user gaining VIP status.
    /// </summary>
    public required string UserName { get; init; }
}
