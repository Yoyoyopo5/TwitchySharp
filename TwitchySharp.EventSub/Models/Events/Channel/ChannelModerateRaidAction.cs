using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Raid"/> action.
/// </summary>
public record ChannelModerateRaidAction : IHaveUser
{
    /// <summary>
    /// The user id of the broadcaster (channel) being raided.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) being raided.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) being raided.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The viewer count.
    /// </summary>
    /// <remarks>
    /// Dev Note: I'm not sure if this is viewer count of the stream at the moment the raid is started,
    /// or if it's the amount of viewers joining the raid.
    /// </remarks>
    public required int ViewerCount { get; init; }
}
