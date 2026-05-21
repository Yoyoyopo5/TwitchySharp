namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelBitsUse"/> event.
/// </summary>
public record ChannelBitsUseEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the bits were used.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the bits were used.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the bits were used.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that used the bits.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that used the bits.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that used the bits.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The number of bits that were used.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The type of bits use.
    /// </summary>
    public required ChannelBitsUseType Type { get; init; }
    /// <summary>
    /// The message associated with the bits use, if any.
    /// </summary>
    public BitsChatMessage? Message { get; init; }
    /// <summary>
    /// The power-up associated with the bits use, if any.
    /// </summary>
    public BitsPowerUp? PowerUp { get; init; }
}
