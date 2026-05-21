namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.DropEntitlementGrant"/>.
/// </summary>
public record DropEntitlementGrantCondition
{
    /// <summary>
    /// The id of the organization that owns the category (game) on the developer portal.
    /// </summary>
    public required OrganizationId OrganizationId { get; init; }
    /// <summary>
    /// The id of the category (game) that this notification is for.
    /// </summary>
    public GameId? CategoryId { get; init; }
    /// <summary>
    /// The id of the drops campaign that this notification is for.
    /// </summary>
    public DropsCampaignId? CampaignId { get; init; }
}
