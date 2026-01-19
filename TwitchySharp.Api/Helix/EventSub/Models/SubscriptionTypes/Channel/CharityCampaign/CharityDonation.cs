using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// Sends an event notification when a user donates to the broadcaster’s charity campaign.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when users donate to their campaign.</param>
public sealed record CharityDonation(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHARITY_DONATION;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
