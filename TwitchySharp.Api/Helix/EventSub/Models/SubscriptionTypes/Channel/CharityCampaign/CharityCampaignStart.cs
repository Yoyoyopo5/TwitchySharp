using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Sends an event notification when the broadcaster starts a charity campaign.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when they start a charity campaign.</param>
public sealed record CharityCampaignStart(UserId BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionTypeName Name { get; } = new(EventSubSubscriptionTypeNames.CHARITY_CAMPAIGN_START);
    public EventSubSubscriptionTypeVersion Version { get; } = new(EventSubSubscriptionTypeVersions.V1);

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
