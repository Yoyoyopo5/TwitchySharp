namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a raid chat notification.
/// </summary>
/// <remarks>
/// Dev Note: I'm not sure if this is for an incoming raid or outgoing one, but docs word it as outgoing.
/// </remarks>
public record ChannelChatNotificationRaid
{
    /// <summary>
    /// The id of the user raiding the channel.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the user raiding the channel.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the user raiding the channel.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The number of viewers in the raid.
    /// </summary>
    public required int ViewerCount { get; init; }
    /// <summary>
    /// The profile image URL of the user raiding the channel.
    /// </summary>
    public required ImageUrl ProfileImageUrl { get; init; }
}
