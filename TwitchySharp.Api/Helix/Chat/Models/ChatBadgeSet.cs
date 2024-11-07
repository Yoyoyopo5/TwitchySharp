namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains a list of <see cref="ChatBadge"/> in a given set.
/// For example, a set may include subscriber badges.
/// </summary>
public record ChatBadgeSet
{
    /// <summary>
    /// An ID that identifies this set of chat badges. 
    /// For example, Bits or Subscriber.
    /// </summary>
    public required string SetId { get; init; }
    /// <summary>
    /// The list of chat badges in this set.
    /// The list is sorted in ascending order by <see cref="ChatBadge.Id"/>.
    /// </summary>
    public required ChatBadge[] Versions { get; init; }
}
