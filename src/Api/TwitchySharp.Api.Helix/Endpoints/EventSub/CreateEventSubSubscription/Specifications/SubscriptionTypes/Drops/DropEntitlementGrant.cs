namespace TwitchySharp.Api.Helix.EventSub.Drops;
/// <summary>
/// An entitlement for a Drop is granted to a user.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported by the webhooks transport. It cannot be used with WebSockets.
/// Requires an app access token created by a client id that is owned by a member of the specified organization.
/// </remarks>
/// <param name="OrganizationId">The organization ID of the organization that owns the game on the developer portal.</param>
/// <param name="CategoryId">The category (or game) ID of the game for which entitlement notifications will be received.</param>
/// <param name="CampaignId">The campaign ID for a specific campaign for which entitlement notifications will be received.</param>
public sealed record DropEntitlementGrant(OrganizationId OrganizationId, GameId? CategoryId = null, DropsCampaignId? CampaignId = null)
    : IEventSubSubscriptionTypeSpecification
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.DropEntitlementGrant;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new("organization_id"), OrganizationId)
            .Set(new("category_id"), CategoryId)
            .Set(new("campaign_id"), CampaignId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
