namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Raid"/> action.
/// </summary>
public record ChannelModerateRaidAction
{
    /// <summary>
    /// The user id of the broadcaster (channel) being raided.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) being raided.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) being raided.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The viewer count.
    /// </summary>
    /// <remarks>
    /// Dev Note: I'm not sure if this is viewer count of the stream at the moment the raid is started,
    /// or if it's the amount of viewers joining the raid.
    /// </remarks>
    public required int ViewerCount { get; init; }
}
