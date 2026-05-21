namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUpdate"/> event.
/// </summary>
public record ChannelUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that changed their channel information.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that changed their channel information.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that changed their channel information.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The updated title of the broadcaster's channel.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The updated language of the broadcaster's channel.
    /// </summary>
    public required LanguageCode Language { get; init; }
    /// <summary>
    /// The updated id of the category (game) of the broadcaster's channel.
    /// </summary>
    public required GameId CategoryId { get; init; }
    /// <summary>
    /// The updated name of the category (game) of the broadcaster's channel.
    /// </summary>
    public required string CategoryName { get; init; }
    /// <summary>
    /// The updated content classification labels currently applied to the broadcaster's channel.
    /// </summary>
    public required ContentClassificationLabelId[] ContentClassificationLabels { get; init; }
}
