namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelWarningSend"/> event.
/// </summary>
public record ChannelWarningSendEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the warning was issued.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the warning was issued.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the warning was issued.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator who sent the warning.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who sent the warning.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who sent the warning.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The id of the user that received the warning.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that received the warning.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that received the warning.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The reason given for the warning by the moderator, if any.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// The chat rules cited for the warning by the moderator, if any.
    /// </summary>
    public string[]? ChatRulesCited { get; init; }
}
