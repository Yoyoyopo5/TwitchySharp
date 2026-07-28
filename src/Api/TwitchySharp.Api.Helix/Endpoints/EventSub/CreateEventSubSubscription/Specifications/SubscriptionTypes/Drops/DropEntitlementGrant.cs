using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
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
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<DropEntitlementGrant>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.DropEntitlementGrant;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.DropEntitlementGrant;
    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("organization_id"), OrganizationId)
            .Set(new("category_id"), CategoryId)
            .Set(new("campaign_id"), CampaignId);

    public static Validation<DropEntitlementGrant> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("organization_id"), out OrganizationId organizationId, value => new(value))
            .GetValue(new("category_id"), out GameId? categoryId, value => new(value))
            .GetValue(new("campaign_id"), out DropsCampaignId? campaignId, value => new(value))
            .Map(_ => new DropEntitlementGrant(organizationId, categoryId, campaignId));
}
