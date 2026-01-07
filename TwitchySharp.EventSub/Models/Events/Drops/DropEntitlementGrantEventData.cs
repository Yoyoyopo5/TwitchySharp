using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Drops;

/// <summary>
/// Contains information about a specific Twitch drops entitlement grant event.
/// </summary>
public record DropEntitlementGrantEventData : IHaveUser
{
    /// <summary>
    /// The id of the organization that owns the category (game) that the drop is for.
    /// </summary>
    public required string OrganizationId { get; init; }
    /// <summary>
    /// The id of the category (game) that the drop is for.
    /// </summary>
    public required string CategoryId { get; init; }
    /// <summary>
    /// The name of the category (name).
    /// </summary>
    public required string CategoryName { get; init; }
    /// <summary>
    /// The Drops campaign the entitlement is associated with.
    /// </summary>
    public required string CampaignId { get; init; }
    /// <summary>
    /// The id of the user who was granted the drop entitlement.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user who was granted the drop entitlement.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the user who was granted the drop entitlement.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The id of the drop entitlement.
    /// </summary>
    public required string EntitlementId { get; init; }
    /// <summary>
    /// The id of the benefit.
    /// </summary>
    public required string BenefitId { get; init; }
    /// <summary>
    /// The date and time when this drop entitlement was granted on Twitch.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
