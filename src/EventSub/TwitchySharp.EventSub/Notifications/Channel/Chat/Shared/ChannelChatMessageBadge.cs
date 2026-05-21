namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific badge displayed next to a chatter's display name.
/// </summary>
public record ChannelChatMessageBadge
{
    /// <summary>
    /// The id of the set that this badge belongs to
    /// (e.g. <c>Bits</c> or <c>Subscriber</c>).
    /// </summary>
    public required ChatBadgeSetId SetId { get; init; }
    /// <summary>
    /// The id of the badge. 
    /// The exact meaning of this id varies by badge set.
    /// </summary>
    public required ChatBadgeId Id { get; init; }
    /// <summary>
    /// Extra metadata about the badge.
    /// Currently, this tag contains metadata only for subscriber badges, to indicate the number of months the user has been a subscriber.
    /// </summary>
    public required string Info { get; init; }
}
