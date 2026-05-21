namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelRaid"/> event.
/// </summary>
public record ChannelRaidEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that created the raid.
    /// </summary>
    public required UserId FromBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that created the raid.
    /// </summary>
    public required UserLogin FromBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that created the raid.
    /// </summary>
    public required UserName FromBroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the raid.
    /// </summary>
    public required UserId ToBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the raid.
    /// </summary>
    public required UserLogin ToBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the raid.
    /// </summary>
    public required UserName ToBroadcasterUserName { get; init; }
    /// <summary>
    /// The number of viewers in the raid.
    /// </summary>
    public required int Viewers { get; init; }
}
