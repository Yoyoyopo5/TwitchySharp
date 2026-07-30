namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific Twitch drops entitlement grant event.
/// </summary>
public record DropEntitlementGrantEventData
{
    /// <summary>
    /// The id of the organization that owns the category (game) that the drop is for.
    /// </summary>
    public required OrganizationId OrganizationId { get; init; }
    /// <summary>
    /// The id of the category (game) that the drop is for.
    /// </summary>
    public required GameId CategoryId { get; init; }
    /// <summary>
    /// The name of the category (name).
    /// </summary>
    public required string CategoryName { get; init; }
    /// <summary>
    /// The Drops campaign the entitlement is associated with.
    /// </summary>
    public required DropsCampaignId CampaignId { get; init; }
    /// <summary>
    /// The id of the user who was granted the drop entitlement.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the user who was granted the drop entitlement.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the user who was granted the drop entitlement.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The id of the drop entitlement.
    /// </summary>
    public required DropsEntitlementId EntitlementId { get; init; }
    /// <summary>
    /// The id of the benefit.
    /// </summary>
    public required DropsBenefitId BenefitId { get; init; }
    /// <summary>
    /// The date and time when this drop entitlement was granted on Twitch.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
