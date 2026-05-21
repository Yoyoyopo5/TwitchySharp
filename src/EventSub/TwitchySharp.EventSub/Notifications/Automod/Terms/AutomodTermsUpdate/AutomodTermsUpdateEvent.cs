namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.AutomodTermsUpdate"/> event.
/// </summary>
public record AutomodTermsUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the Automod terms were updated for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the Automod terms were updated for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the Automod terms were updated for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the Automod terms.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the Automod terms.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the Automod terms.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The status change applied to the terms.
    /// </summary>
    public required AutomodTermsUpdateAction Action { get; init; }
    /// <summary>
    /// Inidicates whether this term was added due to an Automod message approve/deny action.
    /// </summary>
    public required bool FromAutomod { get; init; }
    /// <summary>
    /// The list of the terms that had a status change.
    /// </summary>
    public required string[] Terms { get; init; }
}
